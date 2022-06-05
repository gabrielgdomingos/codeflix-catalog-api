using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.CodeFlix.Catalog.Infrastructure.Persistence.EF.Configurations
{
    internal class CategoryConfiguration
        : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Name)
                .HasMaxLength(255);

            builder.Property(category => category.Description)
                .HasMaxLength(10000);
        }
    }
}
