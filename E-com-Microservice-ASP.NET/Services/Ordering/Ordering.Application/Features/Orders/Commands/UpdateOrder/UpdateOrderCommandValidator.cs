using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} should not be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("{Username} should be shorter than 50 character");

            RuleFor(p => p.EmailAddress)
                .NotNull().WithMessage("{EmailAddress} should not be empty")
                .NotEmpty();

            RuleFor(p => p.TotalPrice)
                .NotNull().WithMessage("{TotalPrice} should not be empty")
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
