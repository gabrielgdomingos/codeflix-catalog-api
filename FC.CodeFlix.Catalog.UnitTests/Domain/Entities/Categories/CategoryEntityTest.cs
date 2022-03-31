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
            var category = new
            {
                Name = "Category Name",
                Description = "Category Description"
            };

            //Act
            var act = new CategoryEntity(category.Name, category.Description);

            //Assert
            Assert.NotNull(act);
            Assert.Equal(category.Name, act.Name);
            Assert.Equal(category.Description, act.Description);
            Assert.NotEqual(default, act.Id);
            Assert.NotEqual(default, act.CreatedAt);
            Assert.True(act.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateWithActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithActive(bool isActive)
        {
            //Arrange
            var category = new
            {
                Name = "Category Name",
                Description = "Category Description"
            };

            //Act
            var act = new CategoryEntity(category.Name, category.Description, isActive);

            //Assert
            Assert.NotNull(act);
            Assert.Equal(category.Name, act.Name);
            Assert.Equal(category.Description, act.Description);
            Assert.NotEqual(default, act.Id);
            Assert.NotEqual(default, act.CreatedAt);
            Assert.Equal(isActive, act.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            //Arrange
            var category = new
            {
                Name = name,
                Description = "Category Description"
            };

            //Act
            var act = () => new CategoryEntity(name!, category.Description);

            //Assert
            var exception =  Assert.Throws<EntityValidationException>(act);

            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameDescriptionIsNull()
        {
            //Arrange
            var category = new
            {
                Name = "Category Name"
            };

            //Act
            var act = () => new CategoryEntity(category.Name, null!);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(act);

            Assert.Equal("Description should not be null", exception.Message);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("a")]
        [InlineData("ab")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string name)
        {
            //Arrange
            var category = new
            {
                Name = name,
                Description = "Category Description"
            };

            //Act
            var act = () => new CategoryEntity(name!, category.Description);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(act);

            Assert.Equal("Name should be at leats 3 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            //Arrange
            var category = new
            {
                Name = invalidName,
                Description = "Category Description"
            };

            //Act
            var act = () => new CategoryEntity(category.Name, category.Description);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(act);

            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
        {
            var invalidDescription = string.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());

            //Arrange
            var category = new
            {
                Name = "Category Name",
                Description = invalidDescription
            };

            //Act
            var act = () => new CategoryEntity(category.Name, category.Description);

            //Assert
            var exception = Assert.Throws<EntityValidationException>(act);

            Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
        }
    }
}
