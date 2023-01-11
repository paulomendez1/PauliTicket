using AutoMapper;
using MediatR;
using PauliTicket.Application.Contracts.Infrastructure;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Models.Mail;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
        }
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);

            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);
            if(validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            @event = await _eventRepository.AddAsync(@event);

            var email = new Email() { To = "paulooo_rc@hotmail.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception)
            {
                //TODO log exception
            }
            return @event.EventId;
        }
    }
}
