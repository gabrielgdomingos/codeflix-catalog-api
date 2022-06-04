﻿using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.GetCategory
{
    [CollectionDefinition(nameof(GetCategoryUseCaseTestFixture))]
    public class CreateCategoryUseCaseTestFixtureCollection
        : ICollectionFixture<GetCategoryUseCaseTestFixture>
    { }

    public class GetCategoryUseCaseTestFixture
        : TestFixtureBase
    {
        public GetCategoryUseCaseTestFixture()
            : base() { }

        public Mock<ICategoryRepository> GetRepositoryMock()
           => new();

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

        public CategoryEntity GetValidCategory()
           => new(
               GetValidCategoryName(),
               GetValidCategoryDescription(),
               true
           );
    }
}
