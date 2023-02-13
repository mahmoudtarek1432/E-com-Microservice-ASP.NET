using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Model;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entity;
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
        public readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
                                           IEmailService emailservice, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository= orderRepository?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper= mapper?? throw new ArgumentNullException(nameof(mapper));
            _emailService= emailservice?? throw new ArgumentNullException(nameof(emailservice));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(order);

            _logger.LogInformation($"Order {newOrder.Id} has been created successfully");
            sendEmail(newOrder);
            return newOrder.Id;
        }

        public async void sendEmail(Order order)
        {
            var email = new Email()
            {
                To = "mahmoudps2000@gmail.com",
                Subject = "new order placed",
                Body = $"order was created ID: {order.Id}"
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"order #{order.Id} email wasnt successfully placed due to an email ex: {ex.Message}");
            }
        }
    }
}
