using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PauliTicket.Application.Features.DTOs.Order;
using PauliTicket.Application.Features.Orders.Commands.CreateOrder;
using PauliTicket.Application.Features.Orders.Queries.GetOrderDetail;
using PauliTicket.Application.Features.Orders.Queries.GetOrdersForMonth;

namespace PauliTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getpagedordersformonth", Name = "GetPagedOrdersForMonth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedOrdersForMonthDTO>> GetPagedOrdersForMonth(Guid id, int page, int size)
        {
            var getOrdersForMonthQuery = new GetOrdersForMonthQuery() { Id = id, Page = page, Size = size };
            var dtos = await _mediator.Send(getOrdersForMonthQuery);

            return Ok(dtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
        {
            var id = await _mediator.Send(createOrderCommand);
            return Ok(id);
        }


        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderById(Guid id)
        {
            var getOrderDetailQuery = new GetOrderDetailQuery() { OrderId = id };
            return Ok(await _mediator.Send(getOrderDetailQuery));
        }
    }
}
