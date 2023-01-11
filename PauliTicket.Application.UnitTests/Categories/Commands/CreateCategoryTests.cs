using AutoMapper;
using Moq;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Features.Categories.Commands.CreateCategory;
using PauliTicket.Application.Profiles;
using PauliTicket.Application.UnitTests.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

            await handler.Handle(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(3);
        }
    }
}
