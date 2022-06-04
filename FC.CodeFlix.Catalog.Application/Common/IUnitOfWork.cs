namespace FC.CodeFlix.Catalog.Application.Common
{
    public interface IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken);
    }
}
