using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Categories
{
    [Collection(nameof(CategoryEntityTestFixture))]
    public class CategoryEntityTest
    {
        //Não estou usando a Fixture para criar os objetos dos testes
        private readonly CategoryEntityTestFixture _categoryEntityTestFixture;

        public CategoryEntityTest(CategoryEntityTestFixture categoryEntityTestFixture)
            => _categoryEntityTestFixture = categoryEntityTestFixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            //Arrange           
            var values = _categoryEntityTestFixture.GetValidCategory();

            //Act
            var category = new CategoryEntity(values.Name, values.Description);

            //Assert
            category.Should().NotBeNull();
            category.Name.Should().Be(values.Name);
            category.Description.Should().Be(values.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBe(default);
            category.IsActive.Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateWithActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithActive(bool isActive)
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            //Act
            var category = new CategoryEntity(values.Name, values.Description, isActive);

            //Assert
            category.Should().NotBeNull();
            category.Name.Should().Be(values.Name);
            category.Description.Should().Be(values.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBe(default);
            category.IsActive.Should().Be(isActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            //Act
            var category = () => new CategoryEntity(name!, values.Description, values.IsActive);

            //Assert
            category.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be null or empty");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            //Act
            var category = () => new CategoryEntity(values.Name, null!, values.IsActive);

            //Assert
            category.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should not be null");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("a")]
        [InlineData("ab")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string name)
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            //Act
            var category = () => new CategoryEntity(name, values.Description, values.IsActive);

            //Assert
            category.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Name should be at leats 3 characters long");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            var invalidName = _categoryEntityTestFixture.Faker.Lorem.Letter(256);

            //Act
            var category = () => new CategoryEntity(invalidName, values.Description, values.IsActive);

            //Assert
            category.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should be at most 255 characters long");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            var invalidDescription = _categoryEntityTestFixture.Faker.Lorem.Letter(10001);

            //Act
            var category = () => new CategoryEntity(values.Name, invalidDescription, values.IsActive);

            //Assert
            category.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should be at most 10000 characters long");
        }

        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
            //Arrange
            var values = _categoryEntityTestFixture.GetValidCategory();

            var category = new CategoryEntity(values.Name, values.Description, false);

            var oldIsActive = category.IsActive;

            //Act
            category.Activate();

            //Assert
            oldIsActive.Should().BeFalse();
            category.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(Deactivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Deactivate()
        {
            //Arrange
            var category = _categoryEntityTestFixture.GetValidCategory();

            var oldIsActive = category.IsActive;

            //Act
            category.Deactivate();

            //Assert
            oldIsActive.Should().BeTrue();
            category.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            //Arrange
            var category = _categoryEntityTestFixture.GetValidCategory();

            var currentValues = new
            {
                Id = category.Id,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt
            };

            var newValues = new
            {
                Name = _categoryEntityTestFixture.GetValidCategoryName(),
                Description = _categoryEntityTestFixture.GetValidCategoryDescription()
            };

            //Act
            category.Update(newValues.Name, newValues.Description);

            //Assert
            category.Name.Should().Be(newValues.Name);
            category.Description.Should().Be(newValues.Description);
            category.Id.Should().Be(currentValues.Id);
            category.IsActive.Should().Be(currentValues.IsActive);
            category.CreatedAt.Should().Be(currentValues.CreatedAt);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenAnyParameterIsInvalid))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        //Teste feito apenas com o parâmetro Name para garantir que
        //o Update() está chamando o Validate() na Entidade.
        public void UpdateErrorWhenAnyParameterIsInvalid(string? name)
        {
            //Arrange
            var category = _categoryEntityTestFixture.GetValidCategory();

            //Act
            var action = () => category.Update(name!, category.Description);

            //Assert
            action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be null or empty");
        }
    }
}
