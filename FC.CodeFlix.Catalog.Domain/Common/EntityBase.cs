namespace FC.CodeFlix.Catalog.Domain.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }

        protected EntityBase()
            => Id = Guid.NewGuid();
    }
}
