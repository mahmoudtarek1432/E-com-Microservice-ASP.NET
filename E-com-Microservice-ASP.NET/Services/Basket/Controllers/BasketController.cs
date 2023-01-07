using Basket.API.Entites;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _BasketRepository;
        public BasketController(IBasketRepository BasketRepository)
        {
            _BasketRepository= BasketRepository ?? throw new ArgumentNullException();
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
    }
}
