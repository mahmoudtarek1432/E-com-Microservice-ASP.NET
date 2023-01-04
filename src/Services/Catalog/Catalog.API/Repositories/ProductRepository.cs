using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IproductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                    .Products
                    .Find(p => p.Id == id)
                    .FirstAsync();


        }

        public async Task<IEnumerable<Product>> GetProductByName(string ProductName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, ProductName);

            return await _context
                    .Products
                    .Find(filter)
                    .ToListAsync();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string CategoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, CategoryName);

            return await _context
                    .Products
                    .Find(filter)
                    .ToListAsync();
        }

        public async Task<bool> updateProduct(Product product)
        {
            var UpdateResult = await _context
                             .Products
                             .ReplaceOneAsync(filter: g => g.Id == product.Id, product);
            return UpdateResult.IsAcknowledged 
                   && UpdateResult.ModifiedCount > 0;
        }

        public async Task createProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> deleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _context
                                    .Products
                                    .DeleteManyAsync(filter);

            return deleteResult.IsAcknowledged 
                   && deleteResult.DeletedCount > 0;
                                    
        }

    }
}
