using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Exceptions;
using PauliTicket.Application.Features.DTOs.Category;
using PauliTicket.Application.Features.DTOs.Event;
using PauliTicket.Application.Features.Events.Queries.GetEventDetail;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailDTO>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository; 
        private readonly ILogger<NotFoundException> _logger;

        public GetEventDetailQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository, ILogger<NotFoundException> logger)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<EventDetailDTO> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);

            if(@event == null)
            {
                throw new NotFoundException(nameof(Event), request.Id, _logger);
            }
            var eventDetailDTO = _mapper.Map<EventDetailDTO>(@event);

            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(Event), request.Id, _logger);
            }

            eventDetailDTO.Category = _mapper.Map<CategoryDTO>(category);

            return eventDetailDTO;
        }
    }
}
