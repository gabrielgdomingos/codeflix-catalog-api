using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.Categories.DeleteCategory;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.DeleteCategory
{
    [Collection(nameof(DeleteCategoryUseCaseTestFixture))]
    public class DeleteCategoryUseCaseTest
    {
        private readonly DeleteCategoryUseCaseTestFixture _fixture;

        public DeleteCategoryUseCaseTest(DeleteCategoryUseCaseTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(HandleSuccess))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async void HandleSuccess()
        {
            //Arrange
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var repositoryMock = _fixture.GetRepositoryMock();

            var category = _fixture.GetValidCategory();

            repositoryMock.Setup(
                repository => repository.GetAsync(
                    category.Id,
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(category);

            var useCase = new DeleteCategoryUseCase(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = new DeleteCategoryInput(category.Id);

            //Act
            var output = await useCase.Handle(input, CancellationToken.None);

            //Assert
            repositoryMock.Verify(
                repository => repository.GetAsync(
                    category.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            repositoryMock.Verify(
                repository => repository.DeleteAsync(
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
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async void HandleErrorWhenDoesntExist()
        {
            //Arrange
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var repositoryMock = _fixture.GetRepositoryMock();

            var categoryId = Guid.NewGuid();

            repositoryMock.Setup(
                repository => repository.GetAsync(
                    categoryId,
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((CategoryEntity)null);

            var useCase = new DeleteCategoryUseCase(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = new DeleteCategoryInput(categoryId);

            //Act
            var action = async ()
               => await useCase.Handle(input, CancellationToken.None);

            //Assert
            await action.Should()
              .ThrowAsync<NotFoundException>()
              .WithMessage($"Category '{input.Id}' not found");

            repositoryMock.Verify(
                repository => repository.GetAsync(
                    categoryId,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
