using Basket.API.Entites;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _RedisDataStore;

        public BasketRepository(IDistributedCache RedisDataStore)
        {
            _RedisDataStore = RedisDataStore?? throw new ArgumentNullException(nameof(RedisDataStore));
        }
        public async Task DeleteBasket(string UserName)
        {
            await _RedisDataStore.RemoveAsync(UserName);
        }

        public async Task<ShoppingCart?> GetBasket(string UserName)
        {
            var result = await _RedisDataStore.GetStringAsync(UserName);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(result);
        }

        public Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
        {
            _RedisDataStore.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return GetBasket(basket.UserName);
        }
    }
}
