using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.Repositories;
using FluentAssertions;
using Xunit;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infrastructure.Persistence.EF.Repositories.Categories
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest
        : IDisposable
    {
        private readonly CategoryRepositoryTestFixture _fixture;

        public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
            => _fixture = fixture;

        public void Dispose()
        {
            //Necessário implementar IDisposable que é chamado após a execução de cada teste
            //Os dados inseridos num teste estavam atrapalhando na execução do outro
            //Além disso foi necessário adicionar o arquivo xunit.runner.json para desabilitar o paralelismo
            _fixture.CleanInMemoryDatabase();
        }

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

            var dbContextDiff = _fixture.GetDbContext();

            var dbCategory = await dbContextDiff.Categories.FindAsync(category.Id);

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

            var dbContextDiff = _fixture.GetDbContext();

            var repository = new CategoryRepository(dbContextDiff);

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

            var categoryId = Guid.NewGuid();

            var categories = _fixture.GetValidCategories();

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var dbContextDiff = _fixture.GetDbContext();

            var repository = new CategoryRepository(dbContextDiff);

            //Act
            var dbCategory = await repository.GetAsync(categoryId, CancellationToken.None);

            //Assert      
            dbCategory.Should().BeNull();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void Update()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var category = _fixture.GetValidCategory();

            await dbContext.AddAsync(category);

            var categories = _fixture.GetValidCategories();

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var newValues = new
            {
                Name = _fixture.GetValidCategoryName(),
                Description = _fixture.GetValidCategoryDescription()
            };

            category.Update(newValues.Name, newValues.Description);

            var repository = new CategoryRepository(dbContext);

            //Act
            await repository.UpdateAsync(category, CancellationToken.None);

            await dbContext.SaveChangesAsync();

            var dbContextDiff = _fixture.GetDbContext();

            var dbCategory = await dbContextDiff.Categories.FindAsync(category.Id);

            //Assert
            dbCategory.Should().NotBeNull();
            dbCategory.Id.Should().Be(category.Id);
            dbCategory.Name.Should().Be(newValues.Name);
            dbCategory.Description.Should().Be(newValues.Description);
            dbCategory.CreatedAt.Should().Be(category.CreatedAt);
            dbCategory.IsActive.Should().Be(category.IsActive);
        }

        [Fact(DisplayName = nameof(Delete))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void Delete()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var category = _fixture.GetValidCategory();

            await dbContext.AddAsync(category);

            var categories = _fixture.GetValidCategories(5);

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var repository = new CategoryRepository(dbContext);

            //Act
            await repository.DeleteAsync(category, CancellationToken.None);

            await dbContext.SaveChangesAsync();

            var dbContextDiff = _fixture.GetDbContext();

            var dbCategory = await dbContextDiff.Categories.FindAsync(category.Id);

            //Assert
            dbCategory.Should().BeNull();
        }

        [Fact(DisplayName = nameof(Search))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void Search()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var categories = _fixture.GetValidCategories(15);

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var repository = new CategoryRepository(dbContext);

            var searchInput = new SearchInput(1, 20, "", "", SearchOrderEnum.Asc);

            //Act
            var searchOutput = await repository.SearchAsync(searchInput, CancellationToken.None);

            //Assert
            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Total.Should().Be(categories.Count);
            searchOutput.Items.Should().HaveCount(categories.Count);

            foreach (var item in searchOutput.Items)
            {
                var category = categories.Find(x => x.Id == item.Id);
                item.Should().NotBeNull();
                item.Name.Should().Be(category.Name);
                item.Description.Should().Be(category.Description);
                item.CreatedAt.Should().Be(category.CreatedAt);
                item.IsActive.Should().Be(category.IsActive);
            }
        }

        [Fact(DisplayName = nameof(SearchEmpty))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        public async void SearchEmpty()
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var repository = new CategoryRepository(dbContext);

            var searchInput = new SearchInput(1, 20, "", "", SearchOrderEnum.Asc);

            //Act
            var searchOutput = await repository.SearchAsync(searchInput, CancellationToken.None);

            //Assert
            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Total.Should().Be(0);
            searchOutput.Items.Should().HaveCount(0);
        }

        [Theory(DisplayName = nameof(SearchPaginated))]
        [Trait("Integration/Infrastructure.Persistence.EF", "CategoryRepository - Repositories")]
        [InlineData(10, 1, 5, 5)]
        [InlineData(10, 2, 5, 5)]
        [InlineData(7, 2, 5, 2)]
        [InlineData(7, 3, 5, 0)]
        public async void SearchPaginated
        (
            int numberOfCategoriesGenerated,
            int currentPage,
            int numberOfItemsPerPage,
            int numberOfExpectedItems
        )
        {
            //Arrange
            var dbContext = _fixture.GetDbContext();

            var categories = _fixture.GetValidCategories(numberOfCategoriesGenerated);

            await dbContext.AddRangeAsync(categories);

            await dbContext.SaveChangesAsync();

            var repository = new CategoryRepository(dbContext);

            var searchInput = new SearchInput(currentPage, numberOfItemsPerPage, "", "", SearchOrderEnum.Asc);

            //Act
            var searchOutput = await repository.SearchAsync(searchInput, CancellationToken.None);

            //Assert
            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Total.Should().Be(categories.Count);
            searchOutput.Items.Should().HaveCount(numberOfExpectedItems);

            foreach (var item in searchOutput.Items.ToList())
            {
                var category = categories.Find(x => x.Id == item.Id);
                item.Should().NotBeNull();
                item.Name.Should().Be(category.Name);
                item.Description.Should().Be(category.Description);
                item.CreatedAt.Should().Be(category.CreatedAt);
                item.IsActive.Should().Be(category.IsActive);
            }
        }
    }
}
