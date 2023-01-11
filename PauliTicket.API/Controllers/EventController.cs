using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PauliTicket.API.Utility;
using PauliTicket.Application.Features.DTOs;
using PauliTicket.Application.Features.DTOs.Event;
using PauliTicket.Application.Features.Events.Commands.CreateEvent;
using PauliTicket.Application.Features.Events.Commands.DeleteEvent;
using PauliTicket.Application.Features.Events.Commands.UpdateEvent;
using PauliTicket.Application.Features.Events.Queries.GetEventDetail;
using PauliTicket.Application.Features.Events.Queries.GetEventsExport;
using PauliTicket.Application.Features.Events.Queries.GetEventsList;
using PauliTicket.Application.Features.Events.Queries.GetEventsListForCategories;

namespace PauliTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <response code="200">Returns a list of events</response>
        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ResponseCache(CacheProfileName = "120SecondsCacheProfile")]
        public async Task<ActionResult<List<EventListDTO>>> GetAllEvents()
        {
            var dtos = await _mediator.Send(new GetEventsListQuery());
            return Ok(dtos);
        }

        /// <summary>
        /// Get events for specific categories provided
        /// </summary>
        /// <param name="categories">The list of categories provided to get events</param>
        /// <response code="200">Returns a list of event taking in consideration the categories provided</response>
        [HttpGet("EventsForCategories", Name = "GetEventsForCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ResponseCache(CacheProfileName = "120SecondsCacheProfile")]
        public async Task<ActionResult<List<EventListDTO>>> GetEventsForSpecificCategories([FromQuery] string[] categories)
        {
            var dtos = await _mediator.Send(new GetEventListForCategoriesQuery() { Categories = categories });
            return Ok(dtos);
        }

        /// <summary>
        /// Get an specific event
        /// </summary>
        /// <param name="id">The id of the event to obtain</param>
        /// <response code="200">Returns an specific event</response>
        /// <response code="400">Returns a 404 code since no event was found</response>
        [HttpGet("{id}", Name = "GetEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EventDetailDTO>> GetEventById(Guid id)
        {
            var getEventDetailQuery = new GetEventDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getEventDetailQuery));
        }

        /// <summary>
        /// Create a event
        /// </summary>
        /// <param name="createEventCommand">The event provided to create it</param>
        /// <response code="200">Returns the id of the event created</response>
        [HttpPost(Name = "AddEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand)
        {
            var id = await _mediator.Send(createEventCommand);
            return Ok(id);
        }

        /// <summary>
        /// Update a specific event
        /// </summary>
        /// <param name="updateEventCommand">The event provided to update</param>
        /// <response code="204">Returns nothing when event updated</response>
        [HttpPut(Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
        {
            await _mediator.Send(updateEventCommand);
            return NoContent();
        }

        /// <summary>
        /// Delete an specific event
        /// </summary>
        /// <param name="id">The id of the event to delete</param>
        /// <response code="204">Returns nothing when event deleted</response>
        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteEventCommand() { EventId = id };
            await _mediator.Send(deleteEventCommand);
            return NoContent();
        }

        /// <summary>
        /// Export to csv all events
        /// </summary>
        /// <response code="200">Returns a csv file of events</response>
        [HttpGet("export", Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents()
        {
            var fileDTO = await _mediator.Send(new GetEventsExportQuery());

            return File(fileDTO.Data, fileDTO.ContentType, fileDTO.EventExportFileName);
        }

    
    }
}
