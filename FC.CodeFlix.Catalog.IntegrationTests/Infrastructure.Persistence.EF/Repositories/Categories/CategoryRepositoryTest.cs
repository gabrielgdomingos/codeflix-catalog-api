using Xunit;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infrastructure.Persistence.EF.Repositories.Categories
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest
    {
        private readonly CategoryRepositoryTestFixture _fixture;

        public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Insert))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void Insert()
        {
            ////Arrange
            //var dbContext = _fixture.GetDbContext();

            //var category = _fixture.GetValidCategory();

            //var repository = new CategoryRepository(dbContext);

            ////Act
            //await repository.InsertAsync(category, CancellationToken.None);

            //await dbContext.SaveChangesAsync(CancellationToken.None);

            ////Assert
            //var dbCategory = await dbContext.Categories.Find(category.Id);

            //dbCategory.Should().NotBeNull();
            //dbCategory.Name.Should().Be(category.Name);
            //dbCategory.Description.Should().Be(category.Description);
            //dbCategory.CreatedAt.Should().Be(category.CreatedAt);
            //dbCategory.IsActive.Should().Be(category.IsActive);
        }
    }
}
