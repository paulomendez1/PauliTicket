using AutoMapper;
using MediatR;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Features.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Orders.Queries.GetOrdersForMonth
{
    public class GetOrdersForMonthQueryHandler : IRequestHandler<GetOrdersForMonthQuery, PagedOrdersForMonthDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public GetOrdersForMonthQueryHandler(IOrderRepository orderRepository, IMapper mapper, IEventRepository eventRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<PagedOrdersForMonthDTO> Handle(GetOrdersForMonthQuery request, CancellationToken cancellationToken)
        {
            var list = await _orderRepository.GetPagedOrdersForMonth(request.Id, request.Page, request.Size);
            var orders = _mapper.Map<List<OrdersForMonthDTO>>(list);

            foreach (var item in orders)
            {
                var @event = await _eventRepository.GetByIdAsync(item.EventId);
                var eventName = @event.Name;

                item.EventName = eventName;
            }

            var count = await _orderRepository.GetTotalCountOfOrdersForMonth(request.Id);
            return new PagedOrdersForMonthDTO() { Count = count, OrdersForMonth = orders, Page = request.Page, Size = request.Size };
        }
    }
}
