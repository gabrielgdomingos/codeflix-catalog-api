using FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories;
using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
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
            //Arrange
            var repositoryMock = _fixture.GetRepositoryMock();

            var input = new ListCategoriesInput(
                page: 2,
                perPage: 15,
                search: "search-example",
                sort: "name",
                dir: SearchOrderEnum.Asc
            );

            var categories = _fixture.GetValidCategories();

            var searchOutput = new SearchOutput<CategoryEntity>(
                currentPage: input.Page,
                perPage: input.PerPage,
                total: 70,
                items: categories
            );

            repositoryMock.Setup(
                repository => repository.SearchAsync(
                    It.Is<SearchInput>(x =>
                        x.Page == input.Page &&
                        x.PerPage == input.PerPage &&
                        x.Search == input.Search &&
                        x.OrderBy == input.Sort &&
                        x.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(searchOutput);

            var useCase = new ListCategoriesUseCase(repositoryMock.Object);

            //Act
            var output = await useCase.Handle(input, CancellationToken.None);

            //Assert
            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(searchOutput.CurrentPage);
            output.PerPage.Should().Be(searchOutput.PerPage);
            output.Total.Should().Be(searchOutput.Total);
            output.Items.Should().HaveCount(searchOutput.Items.Count);

            output.Items.ToList().ForEach(otp =>
                {
                    var category = searchOutput.Items.First(x => x.Id == otp.Id);
                    otp.Should().NotBeNull();
                    otp.Name.Should().Be(category.Name);
                    otp.Description.Should().Be(category.Description);
                    otp.CreatedAt.Should().Be(category.CreatedAt);
                    otp.IsActive.Should().Be(category.IsActive);
                }
            );

            repositoryMock.Verify(
                repository => repository.SearchAsync(
                    It.Is<SearchInput>(x =>
                        x.Page == input.Page &&
                        x.PerPage == input.PerPage &&
                        x.Search == input.Search &&
                        x.OrderBy == input.Sort &&
                        x.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
