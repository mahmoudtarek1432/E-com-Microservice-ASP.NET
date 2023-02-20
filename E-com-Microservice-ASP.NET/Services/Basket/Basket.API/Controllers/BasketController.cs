using AutoMapper;
using Basket.API.Entites;
using Basket.API.gRPCServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _BasketRepository;
        private readonly DiscountGrpcSerice _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IPublishEndpoint publishEndpoint, IBasketRepository BasketRepository,DiscountGrpcSerice discountGrpcSerice, IMapper mapper)
        {
            _BasketRepository= BasketRepository ?? throw new ArgumentNullException();
            _discountGrpcService = discountGrpcSerice ?? throw new ArgumentNullException();
            _mapper = mapper ?? throw new ArgumentNullException();
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException();
        }

        [HttpGet("{UserName}", Name ="GetUserBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string UserName)
        {
            var basket = await _BasketRepository.GetBasket(UserName);
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateUserBasket([FromBody] ShoppingCart basket)
        {
            await Task.WhenAll(basket.Items.Select(async i =>
            {
                var coupon = await _discountGrpcService.GetDiscountCoupon(i.ProductName);
                i.Price -= coupon.Amount;
            }));

            var UpdatedBasket = await _BasketRepository.UpdateBasket(basket);
            return Ok(UpdatedBasket);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserBasket([FromBody] string UserName)
        {
            await _BasketRepository.DeleteBasket(UserName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basketItem = await _BasketRepository.GetBasket(basketCheckout.UserName);
            if(basketItem == null)
            {
                return BadRequest();
            }

            var basketCheckoutEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

            basketCheckoutEvent.TotalPrice = basketCheckout.TotalPrice;

            await _publishEndpoint.Publish(basketCheckoutEvent);

            await _BasketRepository.DeleteBasket(basketCheckout.UserName);

            return Accepted();
        }
    }
}
