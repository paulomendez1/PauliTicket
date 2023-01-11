using AutoMapper;
using MediatR;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Features.DTOs.Event;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Events.Queries.GetEventsListForCategories
{
    public class GetEventListForCategoriesQueryHandler : IRequestHandler<GetEventListForCategoriesQuery, List<EventListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        public GetEventListForCategoriesQueryHandler(IMapper mapper, IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<EventListDTO>> Handle(GetEventListForCategoriesQuery request, CancellationToken cancellationToken)
        {
            var specificCategories = await _categoryRepository.GetCategoriesByName(request.Categories);
            var events = new List<Event>();
            foreach (var category in specificCategories)
            {
               var eventItem = await _eventRepository.GetEventsByCategoryId(category.CategoryId);
                eventItem.ForEach(x => events.Add(x));
            }
            events = events.OrderBy(x => x.Date).ToList();
            return _mapper.Map<List<EventListDTO>>(events);
        }
    }
}
