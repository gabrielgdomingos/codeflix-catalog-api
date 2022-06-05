using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.Categories.UpdateCategory;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.UpdateCategory
{
    [Collection(nameof(UpdateCategoryUseCaseTestFixture))]
    public class UpdateCategoryUseCaseTest
    {
        private readonly UpdateCategoryUseCaseTestFixture _fixture;

        public UpdateCategoryUseCaseTest(UpdateCategoryUseCaseTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(HandleSuccess))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        public async void HandleSuccess()
        {
            //Arrange
            var repositoryMock = _fixture.GetRepositoryMock();

            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var category = _fixture.GetValidCategory();

            var currentValues = new
            {
                Id = category.Id,
                CreatedAt = category.CreatedAt
            };

            repositoryMock.Setup(
                repository => repository.GetAsync(
                    category.Id,
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(category);

            var useCase = new UpdateCategoryUseCase(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = new UpdateCategoryInput(
                category.Id,
                _fixture.GetValidCategoryName(),
                _fixture.GetValidCategoryDescription(),
                !category.IsActive
            );

            //Act
            var output = await useCase.Handle(input, CancellationToken.None);

            //Assert
            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Id.Should().Be(currentValues.Id);
            output.CreatedAt.Should().Be(currentValues.CreatedAt);
            output.IsActive.Should().Be(input.IsActive);

            repositoryMock.Verify(
                repository => repository.GetAsync(
                    category.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            repositoryMock.Verify(
                repository => repository.UpdateAsync(
                    category,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                unitOfWork => unitOfWork.CommitAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }

        [Fact(DisplayName = nameof(HandleErrorWhenDoesntExist))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        public async void HandleErrorWhenDoesntExist()
        {
            //Arrange
            var repositoryMock = _fixture.GetRepositoryMock();

            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var input = _fixture.GetValidInput();

            repositoryMock.Setup(
                repository => repository.GetAsync(
                    input.Id,
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((CategoryEntity)null!);

            var useCase = new UpdateCategoryUseCase(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            //Act
            var action = async ()
                => await useCase.Handle(input, CancellationToken.None);

            //Assert
            await action.Should()
              .ThrowAsync<NotFoundException>()
              .WithMessage($"Category '{input.Id}' not found");

            repositoryMock.Verify(
                repository => repository.GetAsync(
                    input.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }

        [Theory(DisplayName = nameof(HandleErrorCantUpdateAggregate))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async void HandleErrorCantUpdateAggregate(string name)
        {
            //Arrange
            var repositoryMock = _fixture.GetRepositoryMock();

            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var category = _fixture.GetValidCategory();

            repositoryMock.Setup(
                repository => repository.GetAsync(
                    category.Id,
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(category);

            var useCase = new UpdateCategoryUseCase(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = new UpdateCategoryInput(
                category.Id,
                name,
                _fixture.GetValidCategoryDescription(),
                _fixture.GetRandomBoolean()
            );

            //Act
            var action = async ()
                => await useCase.Handle(input, CancellationToken.None);

            //Assert
            await action.Should()
              .ThrowAsync<EntityValidationException>()
              .WithMessage("Name should not be null or empty");

            repositoryMock.Verify(
                repository => repository.GetAsync(
                    input.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
