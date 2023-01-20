using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IMediator _mediator;

        OrderController(IMediator mediator)
        {
            _mediator = mediator?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{UserName}", Name ="GetUserOrders")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrders(string UserName)
        {
            var query = new GetOrdersListQuery(UserName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }


        [HttpPost("CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody]CheckoutOrderCommand Command)
        {
            var order = await _mediator.Send(Command);
            return Ok(order);
        }

        [HttpPut("updateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{Id}",Name ="DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrder(int Id)
        {
            var Command = new DeleteOrderCommand() { Id = Id };
            var order = await _mediator.Send(Command);
            return NoContent();
        }
    }
}
