using FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infrastructure.Persistence.EF.Repositories.Categories
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest
    {
        private readonly CategoryRepositoryTestFixture _fixture;

        public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Add))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void Add()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var category = _fixture.GetValidCategory();

            var repository = new CategoryRepository(dbContext);

            //Act
            await repository.AddAsync(category, CancellationToken.None);

            await dbContext.SaveChangesAsync();

            var dbCategory = await dbContext.Categories.FirstAsync(x => x.Id == category.Id);

            //Assert
            dbCategory.Should().NotBeNull();
            dbCategory.Id.Should().Be(category.Id);
            dbCategory.Name.Should().Be(category.Name);
            dbCategory.Description.Should().Be(category.Description);
            dbCategory.CreatedAt.Should().Be(category.CreatedAt);
            dbCategory.IsActive.Should().Be(category.IsActive);
        }

        [Fact(DisplayName = nameof(Get))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void Get()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var category = _fixture.GetValidCategory();

            await dbContext.AddAsync(category);

            var categories = _fixture.GetValidCategories();

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var repository = new CategoryRepository(dbContext);

            //Act
            var dbCategory = await repository.GetAsync(category.Id, CancellationToken.None);

            //Assert      
            dbCategory.Should().NotBeNull();
            dbCategory.Id.Should().Be(category.Id);
            dbCategory.Name.Should().Be(category.Name);
            dbCategory.Description.Should().Be(category.Description);
            dbCategory.CreatedAt.Should().Be(category.CreatedAt);
            dbCategory.IsActive.Should().Be(category.IsActive);
        }

        [Fact(DisplayName = nameof(GetNull))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void GetNull()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var categories = _fixture.GetValidCategories();

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var repository = new CategoryRepository(dbContext);

            var categoryId = Guid.NewGuid();

            //Act
            var dbCategory = await repository.GetAsync(categoryId, CancellationToken.None);

            //Assert      
            dbCategory.Should().BeNull();
        }
    }
}
