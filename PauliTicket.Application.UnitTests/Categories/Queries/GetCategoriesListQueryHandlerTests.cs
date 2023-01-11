using AutoMapper;
using Moq;
using PauliTicket.Application.Contracts.Persistence;
using PauliTicket.Application.Features.Categories.Queries.GetCategoriesList;
using PauliTicket.Application.Features.DTOs.Category;
using PauliTicket.Application.Profiles;
using PauliTicket.Application.UnitTests.Mocks;
using Shouldly;

namespace PauliTicket.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoriesListQueryHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListDTO>>();

            result.Count.ShouldBe(2);
        }
    }
}
