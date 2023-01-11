using AutoMapper;
using MediatR;
using PauliTicket.Application.Contracts.Infrastructure;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Models.Mail;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _emailService = emailService;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var @order = _mapper.Map<Order>(request);

            @order.OrderPlaced = DateTime.UtcNow;

            @order = await _orderRepository.AddAsync(@order);

            var email = new Email() { To = "paulooo_rc@hotmail.com", Body = $"A new order was created: {request}", Subject = "A new order was created" };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception)
            {
                //TODO log exception
            }
            return @order.OrderId;
        }
    }
}
