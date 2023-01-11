using PauliTicket.Domain.Entities;
using PauliTicket.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(PauliTicketDbContext context)
        {
            context.Categories.Add(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Concerts"
            });
            context.Categories.Add(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Musicals"
            });
            context.Categories.Add(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Plays"
            });
            context.Categories.Add(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Conferences"
            });

            context.SaveChanges();
        }
    }
}
