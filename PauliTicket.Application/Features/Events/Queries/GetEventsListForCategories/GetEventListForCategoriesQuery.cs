using MediatR;
using PauliTicket.Application.Features.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Events.Queries.GetEventsListForCategories
{
    public class GetEventListForCategoriesQuery : IRequest<List<EventListDTO>>
    {
        public string[] Categories { get; set; }
    }
}
