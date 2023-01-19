using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommanderHandler : IRequestHandler<UpdateOrderCommand>
    {
        public readonly IOrderRepository _OrderRepository;
        public readonly IMapper _mapper;
        public readonly ILogger<UpdateOrderCommanderHandler> _logger;

        public UpdateOrderCommanderHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommanderHandler> logger)
        {
            _OrderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var OrderToUpdate = await _OrderRepository.GetByIdAsync(request.Id);
            if(OrderToUpdate == null)
            {
                _logger.LogInformation("Order does not exist in database");
                throw new ArgumentNullException();
            }
            _mapper.Map(request, OrderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _OrderRepository.UpdateAsync(OrderToUpdate);

            _logger.LogInformation($"Order ID: {OrderToUpdate.Id} has been updated succefully");

            return Unit.Value;
        }
    }
}
