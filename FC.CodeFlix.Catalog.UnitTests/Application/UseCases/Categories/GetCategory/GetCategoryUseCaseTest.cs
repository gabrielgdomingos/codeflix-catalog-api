//using Moq;
//using System;
//using System.Threading;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.GetCategory
{
    [Collection(nameof(GetCategoryUseCaseTestFixture))]
    public class GetCategoryUseCaseTest
    {
        private readonly GetCategoryUseCaseTestFixture _fixture;

        public GetCategoryUseCaseTest(GetCategoryUseCaseTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(HandleSuccess))]
        [Trait("Application", "GetCategory - Use Cases")]
        public async void HandleSuccess()
        {
            ////Arrange
            //var repositoryMock = _fixture.GetRepositoryMock();

            //var category = _fixture.GetValidCategory();

            //repositoryMock.Setup(
            //    repository => repository.GetAsync(
            //        It.IsAny<Guid>(),
            //        It.IsAny<CancellationToken>()
            //    )
            //).ReturnsAsync(category);

            //var input = new GetCategoryInput(category.Id);

            //var useCase = new CreateCategoryUseCase(repositoryMock.Object);

            ////Act
            //var output = await useCase.Handle(input, CancellationToken.None);

            ////Assert
            //output.Should().NotBeNull();
            //output.Name.Should().Be(category.Name);
            //output.Description.Should().Be(category.Description);
            //output.Id.Should().Be(category.Id);
            //output.CreatedAt.Should().Be(category.CreatedAt);
            //output.IsActive.Should().Be(category.IsActive);

            //repositoryMock.Verify(
            //    repository => repository.GetAsync(
            //        It.IsAny<Guid>(),
            //        It.IsAny<CancellationToken>()
            //    ),
            //    Times.Once
            //);
        }
    }
}
