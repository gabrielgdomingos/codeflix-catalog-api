using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using System;
using System.Linq;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entities.Categories
{
    public class CategoryEntityTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = "Category Description",
                IsActive = true
            };

            //Act
            var category = new CategoryEntity(values.Name, values.Description, values.IsActive);

            //Assert
            Assert.NotNull(category);
            Assert.Equal(values.Name, category.Name);
            Assert.Equal(values.Description, category.Description);
            Assert.NotEqual(default, category.Id);
            Assert.NotEqual(default, category.CreatedAt);
            Assert.True(category.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateWithActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithActive(bool isActive)
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = "Category Description",
                IsActive = isActive
            };

            //Act
            var category = new CategoryEntity(values.Name, values.Description, values.IsActive);

            //Assert
            Assert.NotNull(category);
            Assert.Equal(values.Name, category.Name);
            Assert.Equal(values.Description, category.Description);            
            Assert.NotEqual(default, category.Id);
            Assert.NotEqual(default, category.CreatedAt);
            Assert.Equal(values.IsActive, category.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            //Arrange
            var values = new
            {
                Name = name,
                Description = "Category Description",
                IsActive = true
            };

            //Act
            var category = () => new CategoryEntity(values.Name!, values.Description, values.IsActive);

            //Assert
            var exception =  Assert.Throws<EntityValidationException>(category);

            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameDescriptionIsNull()
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                IsActive = true
            };

            //Act
            var category = () => new CategoryEntity(values.Name, null!, values.IsActive);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(category);

            Assert.Equal("Description should not be null", exception.Message);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("a")]
        [InlineData("ab")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string name)
        {
            //Arrange
            var values = new
            {
                Name = name,
                Description = "Category Description",
                IsActive = true
            };

            //Act
            var category = () => new CategoryEntity(values.Name!, values.Description, values.IsActive);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(category);

            Assert.Equal("Name should be at leats 3 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            //Arrange
            var values = new
            {
                Name = invalidName,
                Description = "Category Description",
                IsActive = true
            };

            //Act
            var category = () => new CategoryEntity(values.Name, values.Description, values.IsActive);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(category);

            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
        {
            var invalidDescription = string.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());

            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = invalidDescription,
                IsActive = true
            };

            //Act
            var category = () => new CategoryEntity(values.Name, values.Description, values.IsActive);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(category);

            Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = "Category Description",
                IsActive = false
            };

            var category = new CategoryEntity(values.Name, values.Description, values.IsActive);

            var oldIsActive = category.IsActive;

            //Act
            category.Activate();

            //Assert
            Assert.False(oldIsActive);
            Assert.True(category.IsActive);
        }

        [Fact(DisplayName = nameof(Deactivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Deactivate()
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = "Category Description",
                IsActive = true
            };

            var category = new CategoryEntity(values.Name, values.Description, values.IsActive);

            var oldIsActive = category.IsActive;

            //Act
            category.Deactivate();

            //Assert
            Assert.True(oldIsActive);
            Assert.False(category.IsActive);
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = "Category Description",
                IsActive = true
            };            

            var category = new CategoryEntity(values.Name, values.Description, values.IsActive);

            var newValues = new
            {
                Name = "New Category Name",
                Description = "New Category Description"
            };

            var currentValues = new
            {
                Id = category.Id,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt
            };

            //Act
            category.Update(newValues.Name, newValues.Description);

            //Assert
            Assert.Equal(newValues.Name, category.Name);
            Assert.Equal(newValues.Description, category.Description);
            Assert.Equal(currentValues.Id, category.Id);
            Assert.Equal(currentValues.IsActive, category.IsActive);
            Assert.Equal(currentValues.CreatedAt, category.CreatedAt);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenAnyParameterIsInvalid))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        //Teste feito somente com o parametro Name apenas para garantir
        //que o Update() está chamando o Validate() na Entidade.
        public void UpdateErrorWhenAnyParameterIsInvalid(string? name)
        {
            //Arrange
            var values = new
            {
                Name = "Category Name",
                Description = "Category Description",
                IsActive = true
            };

            var category = new CategoryEntity(values.Name, values.Description, values.IsActive);

            //Act
            var action = () => category.Update(name!, values.Description);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(action);

            Assert.Equal("Name should not be empty or null", exception.Message);
        }
    }
}
