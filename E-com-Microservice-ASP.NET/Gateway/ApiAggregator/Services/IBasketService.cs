using ApiAggregator.Models;

namespace ApiAggregator.Servives
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
