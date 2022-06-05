using FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories;
using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.ListCategories
{
    [CollectionDefinition(nameof(ListCategoriesUseCaseTestFixture))]
    public class CreateCategoryUseCaseTestFixtureCollection
       : ICollectionFixture<ListCategoriesUseCaseTestFixture>
    { }

    public class ListCategoriesUseCaseTestFixture
        : TestFixtureBase
    {
        public ListCategoriesUseCaseTestFixture()
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

        public bool GetRandomBoolean()
            => new Random().NextDouble() < 0.5;

        public CategoryEntity GetValidCategory()
           => new(
               GetValidCategoryName(),
               GetValidCategoryDescription(),
               GetRandomBoolean()
           );

        public List<CategoryEntity> GetValidCategories(int length = 10)
        {
            var categories = new List<CategoryEntity>();

            for (int i = 0; i < length; i++)
                categories.Add(GetValidCategory());

            return categories;
        }

        public ListCategoriesInput GetValidInput()
        {
            var random = new Random();

            return new ListCategoriesInput(
                page: random.Next(1, 10),
                perPage: random.Next(15, 100),
                search: Faker.Commerce.ProductName(),
                sort: Faker.Database.Column(),
                dir: random.Next(0, 10) > 5 ?
                    SearchOrderEnum.Asc :
                    SearchOrderEnum.Desc
            );
        }
    }
}
