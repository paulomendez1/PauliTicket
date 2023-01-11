using Newtonsoft.Json;
using PauliTicket.API.IntegrationTests.Base;
using PauliTicket.Application.Features.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauliTicket.API.IntegrationTests.Controllers
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<StartupExtensions>>
    {
        private readonly CustomWebApplicationFactory<StartupExtensions> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<StartupExtensions> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<CategoryListDTO>>(responseString);

            Assert.IsType<List<CategoryListDTO>>(result);
            Assert.NotEmpty(result);
        }
    }
}
