//using FC.CodeFlix.Catalog.Domain.Entities.Categories;
//using Moq;
//using System.Threading;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.ListCategories
{
    [Collection(nameof(ListCategoriesUseCaseTestFixture))]
    public class ListCategoriesUseCaseTest
    {
        private readonly ListCategoriesUseCaseTestFixture _fixture;

        public ListCategoriesUseCaseTest(ListCategoriesUseCaseTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(HandleSuccess))]
        [Trait("Application", "ListCategories - Use Cases")]
        public async void HandleSuccess()
        {
            ////Arrange
            //var repositoryMock = _fixture.GetRepositoryMock();

            //var input = new ListCategoriesInput(
            //    page: 2,
            //    perPage: 15,
            //    search: "search-example",
            //    sort: "name",
            //    dir: SearchOrderEnum.Asc
            //);

            //var categories = _fixture.GetValidCategories();

            //var searchOutput = new SearchOutput<CategoryEntity>(
            //    currentPage: input.Page,
            //    perPage: input.PerPage,
            //    Items: categories,
            //    Total: 70
            //);

            //repositoryMock.Setup(
            //    repository => repository.SearchAsync(
            //        It.IsAny<SearchInput>(x =>
            //            x.Page == input.Page &&
            //            x.PerPage == input.PerPage &&
            //            x.Search == input.Search &&
            //            x.OrderBy == input.Sort &&
            //            x.Order == input.Dir
            //        ),
            //        It.IsAny<CancellationToken>()
            //    )
            //).ReturnsAsync(searchOutput);

            //var useCase = new ListCategoriesUseCase(repositoryMock.Object);

            ////Act
            //var output = await useCase.Handle(input, CancellationToken.None);

            ////Assert
            //output.Should().NotBeNull();
            //output.Page.Should().Be(searchOutput.CurrentPage);
            //output.PerPage.Should().Be(searchOutput.PerPage);
            //output.Total.Should().Be(searchOutput.Total);
            //output.Items.Should().HaveCount(searchOutput.Items.Count);

            //output.Items.Foreach(otp =>
            //    {
            //        var category = searchOutput.Items.Find(x => x.Id == otp.Id);
            //        otp.Should().NotBeNull();
            //        otp.Name.Should().Be(category.Name);
            //        otp.Description.Should().Be(category.Description);
            //        otp.CreatedAt.Should().Be(category.CreatedAt);
            //        otp.IsActive.Should().Be(category.IsActive);
            //    }
            //);

            //repositoryMock.Verify(
            //    repository => repository.SearchAsync(
            //        It.IsAny<SearchInput>(x =>
            //            x.Page == input.Page &&
            //            x.PerPage == input.PerPage &&
            //            x.Search == input.Search &&
            //            x.OrderBy == input.Sort &&
            //            x.Order == input.Dir
            //        ),
            //        It.IsAny<CancellationToken>()
            //    ),
            //    Times.Once
            //);
        }
    }
}
