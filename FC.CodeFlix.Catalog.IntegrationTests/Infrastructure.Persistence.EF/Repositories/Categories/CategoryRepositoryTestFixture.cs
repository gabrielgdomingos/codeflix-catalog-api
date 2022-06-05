using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.IntegrationTests.Common;
//using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infrastructure.Persistence.EF.Repositories.Categories
{
    [CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTestFixtureCollection
        : ICollectionFixture<CategoryRepositoryTestFixture>
    { }

    public class CategoryRepositoryTestFixture
        : TestFixtureBase
    {
        //public CodeFlixCatalogDbContext GetDbContext()
        //{
        //    return new CodeFlixCatalogDbContext(
        //        new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
        //        .UseInMemoryDatabase("integration-tests-db")
        //        .Options
        //    );
        //}

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

        public bool GetRandomBoolean()
            => new Random().NextDouble() < 0.5;

        public CategoryEntity GetValidCategory()
          => new(
              GetValidCategoryName(),
              GetValidCategoryDescription(),
              GetRandomBoolean()
          );
    }
}
