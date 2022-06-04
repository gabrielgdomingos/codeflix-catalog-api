using FC.CodeFlix.Catalog.Application.UseCases.Categories.GetCategory;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.GetCategory
{
    [Collection(nameof(GetCategoryUseCaseTestFixture))]
    public class GetCategoryInputValidatorTest
    {
        private readonly GetCategoryUseCaseTestFixture _fixture;

        public GetCategoryInputValidatorTest(GetCategoryUseCaseTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(ValidateSuccess))]
        [Trait("Application", "GetCategoryInputValidator - Use Cases")]
        public void ValidateSuccess()
        {
            //Arrange
            var input = new GetCategoryInput(Guid.NewGuid());

            var validator = new GetCategoryInputValidator();

            //Act
            var result = validator.TestValidate(input);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = nameof(InvalidWhenEmptyGuidId))]
        [Trait("Application", "GetCategoryInputValidator - Use Cases")]
        public void InvalidWhenEmptyGuidId()
        {
            //Arrange
            var input = new GetCategoryInput(Guid.Empty);

            var validator = new GetCategoryInputValidator();

            //Act
            var result = validator.TestValidate(input);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
