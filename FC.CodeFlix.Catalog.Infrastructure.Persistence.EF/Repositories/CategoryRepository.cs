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

        private DbSet<CategoryEntity> _categories
            => _dbContext.Categories;

        public CategoryRepository(CodeFlixCatalogDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
            => await _categories.AddAsync(aggregate, cancellationToken);

        public Task DeleteAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
            => Task.FromResult(_categories.Remove(aggregate));

        public async Task<CategoryEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
            => await _categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task UpdateAsync(CategoryEntity aggregate, CancellationToken cancellationToken)
            => Task.FromResult(_categories.Update(aggregate));

        public async Task<SearchOutput<CategoryEntity>> SearchAsync(SearchInput input, CancellationToken cancellationToken)
        {
            var skip = (input.Page - 1) * input.PerPage;

            var queryable = _categories.AsNoTracking();

            if (!string.IsNullOrEmpty(input.Search))
                queryable = queryable.Where(x => x.Name.Contains(input.Search));

            var total = await queryable.CountAsync();

            var items = await queryable
                .Skip(skip)
                .Take(input.PerPage)
                .ToListAsync();

            return new SearchOutput<CategoryEntity>(input.Page, input.PerPage, total, items);
        }
    }
}
