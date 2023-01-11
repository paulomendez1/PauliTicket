using MediatR;
using PauliTicket.Application.Features.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesWithEventsQuery: IRequest<List<CategoryEventListDTO>>
    {
        public bool IncludeHistory { get; set; }
    }
}
