using AutoMapper;
using MediatR;
using PauliTicket.Application.Contracts.Infrastructure;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Features.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileDTO>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IEventRepository eventRepository, IMapper mapper, ICsvExporter csvExporter)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileDTO> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = _mapper.Map<List<EventExportDTO>>((await _eventRepository.ListAllAsync()).OrderBy(x => x.Date));

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDTO = new EventExportFileDTO() { ContentType = "text/csv", Data = fileData, EventExportFileName = $"{Guid.NewGuid()}.csv" };
            return eventExportFileDTO;
        }
    }
}
