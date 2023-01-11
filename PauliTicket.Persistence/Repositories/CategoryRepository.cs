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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PauliTicketDbContext dbContext) : base(dbContext) { }

        public async Task<List<Category>> GetCategoriesByName(string[] categoriesName)
        {
            var categories = new List<Category>();
            foreach (var name in categoriesName)
            {
                await _dbContext.Categories.Where(x => x.Name == name).ForEachAsync(category=> categories.Add(category));
            }
            return categories;
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
        {
            var allCategories = await _dbContext.Categories.Include(x => x.Events).ToListAsync();
            if (!includePassedEvents)
            {
                allCategories.ForEach(p => p.Events.ToList().RemoveAll(c => c.Date < DateTime.Today));
            }
            return allCategories;
        }
    }
}
