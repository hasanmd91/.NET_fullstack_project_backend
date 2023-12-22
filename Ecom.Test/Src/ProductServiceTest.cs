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

        [Theory]
        [ClassData(typeof(SingleProductData))]
        public async void GetOneProductByIdAsync_ShouldReturn_AProduct(Product? response, ProductReadDTO? result, Type? type)
        {
            var repo = new Mock<IProductRepo>();
            repo.Setup(repo => repo.GetOneProductByIdAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            var productService = new ProductService(repo.Object, _mapper);

            if (type is not null)
            {
                await Assert.ThrowsAsync(type, () => productService.GetOneProductByIdAsync(It.IsAny<Guid>()));
            }
            else
            {
                var expectedResult = await productService.GetOneProductByIdAsync(It.IsAny<Guid>());
                Assert.Equivalent(result, expectedResult);
            }
        }

        public class SingleProductData : TheoryData<Product?, ProductReadDTO?, Type?>
        {
            public SingleProductData()
            {
                Product product = new() { };
                ProductReadDTO productReadDTO = _mapper.Map<Product, ProductReadDTO>(product);
                Add(product, productReadDTO, null);
                Add(null, productReadDTO, typeof(CustomException));
            }
        }

        [Theory]
        [ClassData(typeof(DeleteSingleProductData))]
        public async void DeleteOneProductAsyncById_ShouldReturnValidResponse(Product? foundProduct, bool response, bool? result, Type? type)
        {
            var repo = new Mock<IProductRepo>();
            if (foundProduct is not null)
            {
                repo.Setup(repo => repo.DeleteOneProductAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            }
            else
            {
                repo.Setup(repo => repo.DeleteOneProductAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            }
            var productService = new ProductService(repo.Object, _mapper);

            if (type is not null)
            {
                await Assert.ThrowsAsync(type, () => productService.DeleteOneProductAsync(It.IsAny<Guid>()));
            }
            else
            {
                var expectedResult = await productService.DeleteOneProductAsync(It.IsAny<Guid>());
                Assert.Equivalent(result, expectedResult);
            }
        }

        public class DeleteSingleProductData : TheoryData<Product?, bool, bool?, Type?>
        {
            public DeleteSingleProductData()
            {
                Product product = new() { };
                Add(product, true, true, null);
                Add(null, false, false, typeof(CustomException));
            }
        }


        [Theory]
        [ClassData(typeof(CreateProductData))]
        public async void CreateOneProductAsync_ShouldReturnValidResponse(Product product, ProductCreateDTO productCreateDTO, ProductReadDTO expected, Type? exceptionType)
        {
            var repo = new Mock<IProductRepo>();
            repo.Setup(repo => repo.CreateOneProductAsync(It.IsAny<Product>())).ReturnsAsync(product);
            var productService = new ProductService(repo.Object, _mapper);

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => productService.CreateOneProductAsync(productCreateDTO));
            }
            else
            {
                var response = await productService.CreateOneProductAsync(productCreateDTO);
                Assert.Equivalent(expected, response);
            }
        }

        public class CreateProductData : TheoryData<Product?, ProductCreateDTO, ProductReadDTO?, Type?>
        {
            public CreateProductData()
            {
                ProductCreateDTO productCreateDTO = new()
                {
                    Title = "tshirts",
                    Description = "woevn",
                    Price = 10,
                    Quantity = 50,
                    CategoryId = Guid.NewGuid(),
                    Images = new List<Image> { new() { ImageUrl = "string" } }
                };

                Product product = _mapper.Map<ProductCreateDTO, Product>(productCreateDTO);
                ProductReadDTO productReadDTO = _mapper.Map<Product, ProductReadDTO>(product);

                Add(product, productCreateDTO, productReadDTO, null);
                Add(null, productCreateDTO, null, typeof(CustomException));
            }
        }


    }
}