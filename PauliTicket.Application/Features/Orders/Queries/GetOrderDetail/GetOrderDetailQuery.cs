using MediatR;
using PauliTicket.Application.Features.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Orders.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery : IRequest<OrderDetailDTO>
    {
        public Guid OrderId { get; set; }
    }
}
