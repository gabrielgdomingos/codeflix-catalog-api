//using FC.CodeFlix.Catalog.Domain.Entities.Categories;
//using FC.CodeFlix.Catalog.Domain.Repositories;
//using Moq;
//using System.Threading;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryUseCaseTest
    {
        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "CreateCategory - Use Cases")]
        public async void CreateCategory()
        {
            ////Arrange
            //var repositoryMock = new Mock<ICategoryRepository>();

            //var unitOfWorkMock = new Mock<IUnitOfWork>();

            //var useCase = new CreateCategoryUseCase(
            //    unitOfWorkMock.Object,
            //    repositoryMock.Object
            //);

            //var input = new CreateCategoryInput(
            //    "Category Name",
            //    "Category Description",
            //    true
            //);

            ////Act
            //var output = await useCase.Handle(input, CancellationToken.None);

            ////Assert
            //output.Should().NotBeNull();
            //output.Name.Should().Be("Category Name");
            //output.Description.Should().Be("Category Description");
            //output.Id.Should().NotBeEmpty();
            //output.CreatedAt.Should().NotBe(default);
            //output.IsActive.Should().BeTrue();

            //repositoryMock.Verify(
            //    repository => repository.Add(
            //        It.IsAny<CategoryEntity>(),
            //        It.IsAny<CancellationToken>()
            //    ),
            //    Times.Once
            //);

            //unitOfWorkMock.Verify(
            //    unitOfWork => unitOfWork.Commit(
            //        It.IsAny<CancellationToken>()
            //    ),
            //    Times.Once
            //);
        }
    }
}
