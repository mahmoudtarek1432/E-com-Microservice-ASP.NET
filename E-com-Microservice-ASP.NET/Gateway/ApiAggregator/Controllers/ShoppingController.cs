using ApiAggregator.Models;
using ApiAggregator.Servives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IBasketService _basket;
        private readonly ICatalogService _catalog;
        private readonly IOrderService _order;

        public ShoppingController(IBasketService basket, ICatalogService catalog, IOrderService order)
        {
            _basket = basket;
            _catalog = catalog;
            _order = order;
        }

        [HttpGet("{UserName}")]
        public async Task<ShoppingModel> GetShopping(string userName)
        {
            var basket = await _basket.GetBasket(userName);

            await Task.WhenAll(
                basket.Items.Select(async I =>
                {
                    var product = await _catalog.GetCatalog(I.ProductId);
                    I.Description = product.Description;
                    I.Category = product.Category;
                    I.Summary = product.Summary;
                    I.ImageFile = product.ImageFile;
                    basket.Items[basket.Items.IndexOf(I)] = I;
                })
            );

            var orders = await _order.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };
            return shoppingModel;
        }
    }
}
