using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.Common;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.GetCategory
{
    [CollectionDefinition(nameof(GetCategoryUseCaseTestFixture))]
    public class GetCategoryUseCaseTestFixtureCollection
        : ICollectionFixture<GetCategoryUseCaseTestFixture>
    { }

    public class GetCategoryUseCaseTestFixture
        : CategoryUseCaseTestFixtureBase
    { }
}
