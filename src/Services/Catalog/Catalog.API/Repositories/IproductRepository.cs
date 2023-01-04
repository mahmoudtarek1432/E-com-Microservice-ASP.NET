using Catalog.API.Entities;
using System.Collections.Generic;

namespace Catalog.API.Repositories
{
    public interface IproductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();

        public Task<Product> GetProduct(string id);

        public Task<IEnumerable<Product>> GetProductByName(string ProductName);
        public Task<IEnumerable<Product>> GetProductsByCategory(string CategoryName);

        public Task createProduct(Product product);

        public Task<bool> updateProduct(Product product);

        public Task<bool> deleteProduct(string id);

    }
}
