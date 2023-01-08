using Discount.API.Entites;
using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Xml.Linq;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _DiscountRepository;
        private readonly ILogger _logger;

        public DiscountController(IDiscountRepository discountRepository, ILogger logger)
        {
            _DiscountRepository = discountRepository?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }

        [HttpGet("{CouponName}",Name="GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Coupon>> GetCoupon(string CouponName)
        {
            var result = await _DiscountRepository.GetCoupon(CouponName);
            if (result == null)
            {
                _logger.LogError($"Coupon name: {CouponName} is not valid");
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateCoupon([FromBody] Coupon coupon)
        {
            await _DiscountRepository.CreateCoupon(coupon);

            return CreatedAtAction("GetDiscount", new { CouponName=coupon.ProductName}, coupon);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateCoupon([FromBody] Coupon coupon)
        {
            await _DiscountRepository.UpdateCoupon(coupon);
            return CreatedAtAction("GetDiscount", new { CouponName = coupon.ProductName }, coupon);


        }


        [HttpDelete("{CouponName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCoupon(string CouponName)
        {
            return Ok(await _DiscountRepository.DeleteCoupon(CouponName)); 
        }
    }
}
