using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var OrderToDelete = await _orderRepository.GetByIdAsync(request.Id);
            
            if(OrderToDelete == null)
            {
                _logger.LogError("The order does not exit in the database");
                throw new NotFoundException(nameof(OrderToDelete), request.Id);
            }
            await _orderRepository.DeleteAsync(OrderToDelete);
            _logger.LogInformation($"The Order {OrderToDelete.Id} was deleted successfully");
            return Unit.Value;
        }
    }
}
