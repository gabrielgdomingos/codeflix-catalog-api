namespace FC.CodeFlix.Catalog.Domain.Common
{
    public interface IGenericRepository<TAggregate>
        : IRepository
    {
        public Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken);

        public Task<TAggregate> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}
