using BookStore.Domain.Interfances;
using BookStore.Domain.Models;
using BookStore.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Domain.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IBookService> _bookService;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _bookService = new Mock<IBookService>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _bookService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOFCategories_WhenCategoriesExist()
        {
            var categories = CreateCategoryList();

            _categoryRepositoryMock.Setup(c =>
                c.GetAll()).ReturnsAsync(categories);

            var result = await _categoryService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }
        private Category CreateCategory()
        {
            return new Category()
            {
                Id = 1,
                Name = "Test Category 1"
            };
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenCategoriesDoNotExist()
        {
            _categoryRepositoryMock.Setup(c =>
                c.GetAll()).ReturnsAsync((List<Category>)null);

            var result = await _categoryService.GetAll();

            Assert.Null(result);
        }

        [Fact]
        public async void GetAll_ShouldCallGetAllFromRepository_OnlyOnce()
        {
            _categoryRepositoryMock.Setup(c =>
                c.GetAll()).ReturnsAsync((List<Category>)null);

            await _categoryService.GetAll();

            _categoryRepositoryMock.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public async void Update_ShouldUpdateCategory_WhenCategoryNameDoesNotExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                c.Search(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(new List<Category>());
            _categoryRepositoryMock.Setup(c => c.Update(category));

            var result = await _categoryService.Update(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void Update_ShouldNotUpdateCategory_WhenCategoryDoesNotExist()
        {
            var category = CreateCategory();
            var categoryList = new List<Category>()
        {
            new Category()
            {
                Id = 2,
                Name = "Test Category 2"
            }
        };

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(categoryList);

            var result = await _categoryService.Update(category);

            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldCallUpdateFromRepository_OnlyOnce()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(new List<Category>());

            await _categoryService.Update(category);

            _categoryRepositoryMock.Verify(mock => mock.Update(category), Times.Once);
        }

        [Fact]
        public async void Search_ShouldReturnAListOfCategory_WhenCategoriesWithSearchedNameExist()
        {
            var categoryList = CreateCategoryList();
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(c =>
                c.Search(c => c.Name.Contains(categoryName)))
                .ReturnsAsync(categoryList);

            var result = await _categoryService.Search(searchedCategory.Name);

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        public async void Search_ShouldReturnNull_WhenCategoriesWithSearchedNameDoNotExist()
        {
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(c =>
                c.Search(c => c.Name.Contains(categoryName)))
                .ReturnsAsync((IEnumerable<Category>)(null));

            var result = await _categoryService.Search(searchedCategory.Name);

            Assert.Null(result);
        }

        [Fact]
        public async void Search_ShouldCallSearchFromRepository_OnlyOnce()
        {
            var categoryList = CreateCategoryList();
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name.Contains(categoryName)))
                .ReturnsAsync(categoryList);

            await _categoryService.Search(searchedCategory.Name);

            _categoryRepositoryMock.Verify(mock => mock.Search(c => c.Name.Contains(categoryName)), Times.Once);
        }

        private List<Category> CreateCategoryList()
        {
            return new List<Category>()
         {
            new Category()
            {
                Id = 1,
                Name = "Test Category 1"
            },
            new Category()
            {
                Id = 2,
                Name = "Test Category 2"
            },
            new Category()
            {
                Id = 3,
                Name = "Test Category 3"
            }
         };
        }
    }
}
