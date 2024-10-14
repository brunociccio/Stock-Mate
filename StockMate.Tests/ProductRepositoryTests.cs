using Moq;
using MongoDB.Driver;
using StockMate.Models;
using StockMate.Repositories;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMate.Tests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IMongoCollection<Product>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<Product>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockDatabase.Setup(db => db.GetCollection<Product>(It.IsAny<string>(), null))
                .Returns(_mockCollection.Object);

            _productRepository = new ProductRepository(_mockDatabase.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldInsertProduct()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Description = "Test Description", Price = 100 };

            // Act
            await _productRepository.CreateProductAsync(product);

            // Assert
            _mockCollection.Verify(p => p.InsertOneAsync(product, null, default), Times.Once);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var mockProducts = new List<Product> { new Product { Name = "Test Product" } };
            _mockCollection.Setup(p => p.FindAsync(It.IsAny<FilterDefinition<Product>>(), null, default))
                        .ReturnsAsync(Mock.Of<IAsyncCursor<Product>>(cursor => cursor.ToListAsync() == Task.FromResult(mockProducts)));

            // Act
            var result = await _productRepository.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Product", result[0].Name);
        }
    }
}
