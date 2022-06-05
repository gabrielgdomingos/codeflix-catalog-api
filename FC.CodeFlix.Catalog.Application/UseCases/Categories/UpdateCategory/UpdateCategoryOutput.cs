using FC.CodeFlix.Catalog.Domain.Entities.Categories;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryOutput
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; private set; }

        public UpdateCategoryOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public static UpdateCategoryOutput FromEntity(CategoryEntity category)
            => new(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt
            );
    }
}
