using Microsoft.EntityFrameworkCore;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(PauliTicketDbContext dbContext): base(dbContext) { }

        public async Task<List<Event>> GetEventsByCategoryId(Guid categoryId)
        {
            var events = await _dbContext.Events.Where(e => e.CategoryId == categoryId).ToListAsync();
            return events;
        }

        public Task<bool> IsEventNameAndDateUnique(string name, DateTime date)
        {
            var matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(date.Date));
            return Task.FromResult(matches);
        }
    }
}
