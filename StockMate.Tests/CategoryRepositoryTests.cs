using Moq;
using MongoDB.Driver;
using StockMate.Models;
using StockMate.Repositories;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMate.Tests
{
    public class CategoryRepositoryTests
    {
        private readonly Mock<IMongoCollection<Category>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<Category>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockDatabase.Setup(db => db.GetCollection<Category>(It.IsAny<string>(), null))
                .Returns(_mockCollection.Object);

            _categoryRepository = new CategoryRepository(_mockDatabase.Object);
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldInsertCategory()
        {
            // Arrange
            var category = new Category { Name = "Test", Description = "Test Description" };

            // Act
            await _categoryRepository.CreateCategoryAsync(category);

            // Assert
            _mockCollection.Verify(c => c.InsertOneAsync(category, null, default), Times.Once);
        }

        [Fact]
        public async Task GetCategoriesAsync_ShouldReturnListOfCategories()
        {
            // Arrange
            var mockCategories = new List<Category> { new Category { Name = "Test Category" } };
            _mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Category>>(), null, default))
                        .ReturnsAsync(Mock.Of<IAsyncCursor<Category>>(cursor => cursor.ToListAsync() == Task.FromResult(mockCategories)));

            // Act
            var result = await _categoryRepository.GetCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Category", result[0].Name);
        }
    }
}
