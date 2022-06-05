using FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories;
using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.Common;
using System;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.ListCategories
{
    [CollectionDefinition(nameof(ListCategoriesUseCaseTestFixture))]
    public class ListCategoriesUseCaseTestFixtureCollection
       : ICollectionFixture<ListCategoriesUseCaseTestFixture>
    { }

    public class ListCategoriesUseCaseTestFixture
        : CategoryUseCaseTestFixtureBase
    {
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
