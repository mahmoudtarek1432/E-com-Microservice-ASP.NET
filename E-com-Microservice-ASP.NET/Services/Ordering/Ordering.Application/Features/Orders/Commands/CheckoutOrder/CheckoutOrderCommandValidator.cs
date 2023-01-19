using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator() {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} should not be null")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName Shall not be over 50 characters in length");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} should not be empty")
                .NotNull();

            RuleFor(p => p.TotalPrice)
                .GreaterThan(0).WithMessage("{TotalPrice} should not be zero");

        }
    }
}
