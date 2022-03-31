using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Categories
{
    public class CategoryEntityTestFixture
    {
        public CategoryEntity GetValidCategory() 
            => new CategoryEntity("Category Name", "Category Description", true);
    }

    [CollectionDefinition(nameof(CategoryEntityTestFixture))]
    public class CategoryEntityTestFixtureCollection
    : ICollectionFixture<CategoryEntityTestFixture>
    {

    }
}