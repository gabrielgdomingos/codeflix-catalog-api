using FC.CodeFlix.Catalog.Domain.Common.SearchableRepository;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.DbContexts;
using Microsoft.EntityFrameworkCore;

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

        public Task DeleteAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Remove(aggregate));
        }

        public async Task<CategoryEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<SearchOutput<CategoryEntity>> SearchAsync(SearchInput input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Update(aggregate));
        }
    }
}
