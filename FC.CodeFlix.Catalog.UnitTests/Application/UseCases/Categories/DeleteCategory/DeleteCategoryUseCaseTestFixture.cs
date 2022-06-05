using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.Common;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.DeleteCategory
{
    [CollectionDefinition(nameof(DeleteCategoryUseCaseTestFixture))]
    public class DeleteCategoryUseCaseTestFixtureCollection
       : ICollectionFixture<DeleteCategoryUseCaseTestFixture>
    { }

    public class DeleteCategoryUseCaseTestFixture
        : CategoryUseCaseTestFixtureBase
    { }
}
