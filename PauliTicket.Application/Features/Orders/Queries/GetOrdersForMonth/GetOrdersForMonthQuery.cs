using MediatR;
using PauliTicket.Application.Features.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Orders.Queries.GetOrdersForMonth
{
    public class GetOrdersForMonthQuery : IRequest<PagedOrdersForMonthDTO>
    {
        public Guid Id { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
