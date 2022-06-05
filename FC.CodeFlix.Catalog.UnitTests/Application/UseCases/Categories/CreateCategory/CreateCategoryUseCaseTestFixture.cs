using FC.CodeFlix.Catalog.Application.UseCases.Categories.CreateCategory;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.Common;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.CreateCategory
{
    [CollectionDefinition(nameof(CreateCategoryUseCaseTestFixture))]
    public class CreateCategoryUseCaseTestFixtureCollection
        : ICollectionFixture<CreateCategoryUseCaseTestFixture>
    { }

    public class CreateCategoryUseCaseTestFixture
        : CategoryUseCaseTestFixtureBase
    {
        public CreateCategoryInput GetValidInput()
            => new(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                GetRandomBoolean()
            );
    }
}
