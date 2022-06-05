using FC.CodeFlix.Catalog.Application.Common.PaginatedList;
using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesInput
        : PaginatedListInput, IRequest<ListCategoriesOutput>
    {
        public ListCategoriesInput(
            int page = 1,
            int perPage = 15,
            string search = "",
            string sort = "",
            SearchOrderEnum dir = SearchOrderEnum.Asc)
            : base(page, perPage, search, sort, dir)
        { }
    }
}
