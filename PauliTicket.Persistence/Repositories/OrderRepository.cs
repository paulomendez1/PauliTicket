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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(PauliTicketDbContext dbContext) : base(dbContext) { }

        public async Task<List<Order>> GetPagedOrdersForMonth(Guid id, int page, int size)
        {
            return await _dbContext.Orders.Where(x => x.UserId == id)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetTotalCountOfOrdersForMonth(Guid id)
        {
            return await _dbContext.Orders.CountAsync(x => x.UserId == id);
        }
    }
}
