using PauliTicket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; } = default!;
    }
}
