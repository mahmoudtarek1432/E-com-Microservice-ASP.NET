using ApiAggregator.Models;

namespace ApiAggregator.Servives
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
