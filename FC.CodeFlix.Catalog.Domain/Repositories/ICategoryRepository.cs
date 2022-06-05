using FC.CodeFlix.Catalog.Domain.Common;
using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;

namespace FC.CodeFlix.Catalog.Domain.Repositories
{
    public interface ICategoryRepository
        : IGenericRepository<CategoryEntity>, ISearchableRepository<CategoryEntity>
    { }
}
