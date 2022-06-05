namespace FC.CodeFlix.Catalog.Domain.Common.SearchableRepository
{
    public interface ISearchableRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        Task<SearchOutput<TAggregate>> SearchAsync(SearchInput input, CancellationToken cancellationToken);
    }
}
