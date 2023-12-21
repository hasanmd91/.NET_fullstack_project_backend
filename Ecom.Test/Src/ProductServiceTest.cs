using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Newtonsoft.Json;
using Xunit;
namespace Ecom.Test.Src
{
    public class ProductServiceTest
    {
        private static IMapper _mapper;

        public ProductServiceTest()
        {

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            IMapper mapper = configuration.CreateMapper();
            _mapper = mapper;

        }

        [Fact]
        public async void GetAllProducts_withValidLimitAndOffset_ShouldInvokedRepoMethod()
        {
            var repo = new Mock<IProductRepo>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };
            var productService = new ProductService(repo.Object, _mapper);
            await productService.GetAllProductAsync(options);
            repo.Verify(repo => repo.GetAllProductAsync(options), Times.Once);
        }
        [Fact]
        public async void GetAllProductsAsync_ShouldReturnValidResponse()
        {
            var repo = new Mock<IProductRepo>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };

            Product Shoe = new()
            {
                Title = "tshirts",
                Description = "woevn",
                Price = 10,
                Quantity = 50,
                CategoryId = Guid.NewGuid(),
                Images = new List<Image> { new() { ImageUrl = "string" } }
            };
            Product Cloths = new()
            {
                Title = "tshirts",
                Description = "woevn",
                Price = 10,
                Quantity = 50,
                CategoryId = Guid.NewGuid(),
                Images = new List<Image> { new() { ImageUrl = "string" } }
            };

            IEnumerable<Product> repoResponse = new List<Product> { Shoe, Cloths };
            IEnumerable<ProductReadDTO> expected = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(repoResponse);

            repo.Setup(repo => repo.GetAllProductAsync(options)).ReturnsAsync(repoResponse);
            var productService = new ProductService(repo.Object, _mapper);
            var response = await productService.GetAllProductAsync(options);

            Assert.Equivalent(expected, response);
        }


    }
}