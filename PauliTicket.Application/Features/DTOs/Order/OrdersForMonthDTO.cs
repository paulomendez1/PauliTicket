using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.DTOs.Order
{
    public class OrdersForMonthDTO
    {
        public Guid OrderId { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
