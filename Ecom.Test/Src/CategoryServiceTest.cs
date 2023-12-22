using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Moq;

namespace Ecom.Test.Src
{
    public class CategoryServiceTest
    {

        private static IMapper _mapper;

        public CategoryServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            IMapper mapper = configuration.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async void GetAllCatgory_withValidLimitAndOffset_ShouldInvokedRepoMethod()
        {
            var repo = new Mock<ICategoryRepo>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };
            var categoryService = new CategoryService(repo.Object, _mapper);
            await categoryService.GetAllCategoryAsync(options);
            repo.Verify(repo => repo.GetAllCategoryAsync(options), Times.Once);
        }


        [Fact]
        public async void GetAllCategoryAsyncc_ShouldReturnValidResponse()
        {
            var repo = new Mock<ICategoryRepo>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };
            Category category1 = new() { };
            Category category2 = new() { };
            IEnumerable<Category> categories = new List<Category> { category1, category2 };
            IEnumerable<CategoryReadDTO> expected = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>(categories);
            repo.Setup(repo => repo.GetAllCategoryAsync(options)).ReturnsAsync(categories);
            var categoryService = new CategoryService(repo.Object, _mapper);
            var response = await categoryService.GetAllCategoryAsync(options);
            Assert.Equivalent(expected, response);
        }

        [Fact]
        public async Task GetOneCategoryByIdAsync_ShouldReturn_Category()
        {
            var repo = new Mock<ICategoryRepo>();
            var expectedCategory = new Category { Name = "Cloths" };
            var expectedCategoryDTO = _mapper.Map<Category, CategoryReadDTO>(expectedCategory);

            repo.Setup(repo => repo.GetOneCategoryByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedCategory);
            var categoryService = new CategoryService(repo.Object, _mapper);

            var actualCategoryDTO = await categoryService.GetOneCategoryByIdAsync(It.IsAny<Guid>());

            Assert.NotNull(actualCategoryDTO);
            Assert.Equal(expectedCategoryDTO.Name, actualCategoryDTO.Name);
        }

        [Theory]
        [ClassData(typeof(DeleteCategoryData))]
        public async void DeleteOneCategoryAsyncById_ShouldReturnValidResponse(Category? foundcategory, bool response, bool? result, Type? type)
        {
            var repo = new Mock<ICategoryRepo>();
            if (foundcategory is not null)
            {
                repo.Setup(repo => repo.DeleteOneByIdCategoryAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            }
            else
            {
                repo.Setup(repo => repo.DeleteOneByIdCategoryAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            }
            var categoryService = new CategoryService(repo.Object, _mapper);

            if (type is not null)
            {
                await Assert.ThrowsAsync(type, () => categoryService.DeleteOneByIdCategoryAsync(It.IsAny<Guid>()));
            }
            else
            {
                var expectedResult = await categoryService.DeleteOneByIdCategoryAsync(It.IsAny<Guid>());
                Assert.Equivalent(result, expectedResult);
            }
        }

        public class DeleteCategoryData : TheoryData<Category?, bool, bool?, Type?>
        {
            public DeleteCategoryData()
            {
                Category category = new() { };
                Add(category, true, true, null);
                Add(null, false, false, typeof(CustomException));
            }
        }



        [Fact]
        public async Task CreateOneCategoryAsync_ShouldReturnValidResponse()
        {
            var repo = new Mock<ICategoryRepo>();
            var categoryCreateDTO = new CategoryCreateDTO { Name = "shoe" };
            var category = _mapper.Map<CategoryCreateDTO, Category>(categoryCreateDTO);
            var categoryReadDTO = _mapper.Map<Category, CategoryReadDTO>(category);

            repo.Setup(repo => repo.CreateOneCategoryAsync(It.IsAny<Category>())).ReturnsAsync(category);
            var categoryService = new CategoryService(repo.Object, _mapper);

            var response = await categoryService.CreateOneCategoryAsync(categoryCreateDTO);

            Assert.NotNull(response);
            Assert.Equal(categoryReadDTO.Name, response.Name);
        }
    }
}