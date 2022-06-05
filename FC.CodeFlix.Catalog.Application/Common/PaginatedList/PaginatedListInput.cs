using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;

namespace FC.CodeFlix.Catalog.Application.Common.PaginatedList
{
    public abstract class PaginatedListInput
    {
        public int Page { get; set; }

        public int PerPage { get; set; }

        public string Search { get; set; }

        public string Sort { get; set; }

        public SearchOrderEnum Dir { get; set; }

        protected PaginatedListInput(int page, int perPage, string search, string sort, SearchOrderEnum dir)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            Sort = sort;
            Dir = dir;
        }
    }
}
