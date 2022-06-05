using FC.CodeFlix.Catalog.Application.Common.PaginatedList;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesOutput
        : PaginatedListOutput<ListCategoriesOutputModel>
    {

        public ListCategoriesOutput(int currentPage, int perPage, int total, IReadOnlyList<ListCategoriesOutputModel> items)
            : base(currentPage, perPage, total, items)
        {
        }
    }
}
