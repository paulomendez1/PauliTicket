using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Exceptions;
using PauliTicket.Application.Features.DTOs.Order;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Orders.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, OrderDetailDTO>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<NotFoundException> _logger;

        public GetOrderDetailQueryHandler(IMapper mapper, IOrderRepository orderRepository, ILogger<NotFoundException> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<OrderDetailDTO> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var @order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (@order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId, _logger);
            }
            var orderDetailDTO = _mapper.Map<OrderDetailDTO>(@order);

            return orderDetailDTO;
        }
    }
}
