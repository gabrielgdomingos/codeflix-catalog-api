using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Categories
{
    public class CategoryEntityTestFixture 
        : BaseTestFixture
    {
        public CategoryEntityTestFixture()
            : base() { }

        public CategoryEntity GetValidCategory() 
            => new (
                GetValidCategoryName(),
                GetValidCategoryDescription(), 
                true
            );

        public string GetValidCategoryName()
        {
            var name = "ab";

            while (name.Length < 3)
                name = Faker.Commerce.Categories(1)[0];

            if (name.Length > 255)
                name = name[..255];

            return name;
        }

        public string GetValidCategoryDescription()
        {
            var description = Faker.Commerce.ProductDescription();           

            if (description.Length > 10000)
                description = description[..10000];

            return description;
        }
    }

    [CollectionDefinition(nameof(CategoryEntityTestFixture))]
    public class CategoryEntityTestFixtureCollection
        : ICollectionFixture<CategoryEntityTestFixture>
    {}
}