namespace FC.CodeFlix.Catalog.Domain.Common
{
    public interface IGenericRepository<TAggregate>
        : IRepository
    {
        public Task Add(TAggregate aggregate, CancellationToken cancellationToken);
    }
}
