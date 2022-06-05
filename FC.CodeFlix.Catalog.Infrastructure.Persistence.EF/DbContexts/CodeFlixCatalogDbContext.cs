using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.DbContexts
{
    public class CodeFlixCatalogDbContext
        : DbContext
    {
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

        public CodeFlixCatalogDbContext(DbContextOptions<CodeFlixCatalogDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
