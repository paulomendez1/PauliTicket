using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Exceptions;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<NotFoundException> _notFoundLogger;
        public UpdateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, ILogger<NotFoundException> notFoundLogger)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _notFoundLogger = notFoundLogger;
        }
        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);
            if(eventToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId, _notFoundLogger);
            }

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));
            await _eventRepository.UpdateAsync(eventToUpdate);

            return Unit.Value;
        }
    }
}
