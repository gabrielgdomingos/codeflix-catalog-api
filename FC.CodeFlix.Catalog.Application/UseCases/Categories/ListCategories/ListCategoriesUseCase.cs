using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Domain.Repositories;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesUseCase
        : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategoriesUseCase(ICategoryRepository categoryRepository)
            => _categoryRepository = categoryRepository;

        public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
        {
            var searchInput = new SearchInput(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            );

            var searchOutput = await _categoryRepository.SearchAsync(searchInput, cancellationToken);

            var items = ListCategoriesOutputModel.FromEntities(searchOutput.Items.ToList());

            var output = new ListCategoriesOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                items
            );

            return output;
        }
    }
}
