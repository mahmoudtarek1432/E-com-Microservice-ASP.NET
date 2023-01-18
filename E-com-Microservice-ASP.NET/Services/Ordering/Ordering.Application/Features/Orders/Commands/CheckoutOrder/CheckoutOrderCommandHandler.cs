using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IMapper _mapper;
        public readonly IEmailService _emailService;
        public readonly ILogger _

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailservice)
        {
            _orderRepository= orderRepository?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper= mapper?? throw new ArgumentNullException(nameof(mapper));
            _emailService= emailservice?? throw new ArgumentNullException(nameof(emailservice));
        }
        public Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
