using FC.CodeFlix.Catalog.Application.UseCases.Categories.UpdateCategory;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.Common;
using System;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryUseCaseTestFixture))]
    public class UpdateCategoryUseCaseTestFixtureCollection
       : ICollectionFixture<UpdateCategoryUseCaseTestFixture>
    { }

    public class UpdateCategoryUseCaseTestFixture
        : CategoryUseCaseTestFixtureBase
    {
        public UpdateCategoryInput GetValidInput()
               => new(
                   Guid.NewGuid(),
                   GetValidCategoryName(),
                   GetValidCategoryDescription(),
                   GetRandomBoolean()
               );
    }
}