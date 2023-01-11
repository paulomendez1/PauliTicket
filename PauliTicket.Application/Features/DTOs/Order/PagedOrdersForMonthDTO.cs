using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.DTOs.Order
{
    public class PagedOrdersForMonthDTO
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ICollection<OrdersForMonthDTO> OrdersForMonth { get; set; }
    }
}
