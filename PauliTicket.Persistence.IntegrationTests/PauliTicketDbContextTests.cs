using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PauliTicket.Persistence;
using PauliTicket.Application.Contracts;
using Moq;
using PauliTicket.Domain.Entities;
using Shouldly;

namespace PauliTicket.Persistence.IntegrationTests
{
    public class PauliTicketDbContextTests
    {
        private readonly PauliTicketDbContext _pauliTicketDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public PauliTicketDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<PauliTicketDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = "00000000-0000-0000-0000-000000000000";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _pauliTicketDbContext = new PauliTicketDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var ev = new Event() { EventId = Guid.NewGuid(), Name = "Test event" };

            _pauliTicketDbContext.Events.Add(ev);
            await _pauliTicketDbContext.SaveChangesAsync();

            ev.CreatedBy.ShouldBe(_loggedInUserId);
        }
    }
}
