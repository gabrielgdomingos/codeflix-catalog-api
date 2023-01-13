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

            if (!string.IsNullOrEmpty(input.OrderBy))
                queryable = AddOrderToQueryable(queryable, input.OrderBy, input.Order);

            var total = await queryable.CountAsync();

            var items = await queryable
                .Skip(skip)
                .Take(input.PerPage)
                .ToListAsync();

            return new SearchOutput<CategoryEntity>(
                input.Page,
                input.PerPage,
                total,
                items);
        }

        private IQueryable<CategoryEntity> AddOrderToQueryable(IQueryable<CategoryEntity> queryable, string orderBy, SearchOrderEnum sortOrder)
        {
            return (orderBy.ToLower(), sortOrder) switch
            {
                ("id", SearchOrderEnum.Asc) => queryable.OrderBy(x => x.Id),
                ("id", SearchOrderEnum.Desc) => queryable.OrderByDescending(x => x.Id),
                ("name", SearchOrderEnum.Asc) => queryable.OrderBy(x => x.Name),
                ("name", SearchOrderEnum.Desc) => queryable.OrderByDescending(x => x.Name),
                ("createdat", SearchOrderEnum.Asc) => queryable.OrderBy(x => x.Name),
                ("createdat", SearchOrderEnum.Desc) => queryable.OrderByDescending(x => x.Name),
                _ => queryable
            };
        }
    }
}
