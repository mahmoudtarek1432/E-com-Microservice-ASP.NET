using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandeler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        public readonly IOrderRepository _OrderRepository;
        public readonly IMapper _mapper;

        public GetOrdersListQueryHandeler(IOrderRepository orderRepository, IMapper mapper)
        {
            _OrderRepository = orderRepository?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _OrderRepository.GetUserOrders(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orders);
        }
    }
}
