using FC.CodeFlix.Catalog.Application.Common.PaginatedList;
using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesInput
        : PaginatedListInput, IRequest<ListCategoriesOutput>
    {
        public ListCategoriesInput(int page, int perPage, string search, string sort, SearchOrderEnum dir)
            : base(page, perPage, search, sort, dir)
        { }
    }
}
