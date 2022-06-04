using FC.CodeFlix.Catalog.Application.UseCases.Categories.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.CreateCategory
{
    [Collection(nameof(CreateCategoryUseCaseTestFixture))]
    public class CreateCategoryUseCaseTest
    {
        private readonly CreateCategoryUseCaseTestFixture _fixture;

        public CreateCategoryUseCaseTest(CreateCategoryUseCaseTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategory()
        {
            //Arrange
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var repositoryMock = _fixture.GetRepositoryMock();

            var useCase = new CreateCategoryUseCase(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = _fixture.GetValidInput();

            //Act
            var output = await useCase.HandleAsync(input, CancellationToken.None);

            //Assert
            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBe(default);
            output.IsActive.Should().Be(input.IsActive);

            repositoryMock.Verify(
                repository => repository.AddAsync(
                    It.IsAny<CategoryEntity>(),
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
    }
}
