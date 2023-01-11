using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.DTOs.Event
{
    public class EventExportDTO
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
