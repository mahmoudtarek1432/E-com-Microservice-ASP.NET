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
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {

        public readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
                                           IEmailService emailservice, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailservice;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = this._mapper.Map<Order>(request);
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
                _logger.LogInformation($"order #{order.Id} email wasnt successfully placed due to an email ex: {ex.Message}");
            }
        }
    }
}
