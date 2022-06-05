using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.DbContexts;

namespace FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.Repositories
{
    public class CategoryRepository
        : ICategoryRepository
    {

        private readonly CodeFlixCatalogDbContext _dbContext;

        public CategoryRepository(CodeFlixCatalogDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
            => await _dbContext.Categories.AddAsync(aggregate, cancellationToken);

        public async Task DeleteAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryEntity> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SearchOutput<CategoryEntity>> SearchAsync(SearchInput input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
