using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PauliTicket.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Identity
{
    public class PauliTicketIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public PauliTicketIdentityDbContext()
        {
                
        }

        public PauliTicketIdentityDbContext(DbContextOptions<PauliTicketIdentityDbContext> options) : base(options)
        {

        }
    }
}
