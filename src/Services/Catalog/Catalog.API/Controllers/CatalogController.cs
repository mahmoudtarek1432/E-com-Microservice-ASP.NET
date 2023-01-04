using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IproductRepository _productRepo;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IproductRepository productRepo, ILogger<CatalogController> logger) 
        {
            _productRepo = productRepo?? throw new ArgumentNullException(nameof(productRepo));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepo.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepo.GetProduct(id);
            if(product == null)
            {
                _logger.LogError($"product with id:{id} is not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string category)
        {
            var products = await _productRepo.GetProductsByCategory(category);
            if(!products.Any())
            {
                _logger.LogError($"Product {category} is Empty.");
                return NotFound();
            }
            return Ok(products);
        }
    }
}
