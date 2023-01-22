using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entity;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {

        }



        public async Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            var orderList = await _Context.Orders
                                          .Where(o => UserName == UserName)
                                          .ToListAsync();
            return orderList;
        }
    }
}
