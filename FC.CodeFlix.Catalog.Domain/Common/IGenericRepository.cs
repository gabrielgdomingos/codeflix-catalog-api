namespace FC.CodeFlix.Catalog.Domain.Common
{
    public interface IGenericRepository<TAggregate>
        : IRepository where TAggregate : AggregateRoot
    {
        public Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken);

        public Task<TAggregate> GetAsync(Guid id, CancellationToken cancellationToken);

        public Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken);

        public Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken);
    }
}
