using FC.CodeFlix.Catalog.Domain.Entities.Categories;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesOutputModel
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; private set; }

        public ListCategoriesOutputModel(Guid id, string name, string description, bool isActive, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public static ListCategoriesOutputModel FromEntity(CategoryEntity category)
            => new(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt
            );

        public static List<ListCategoriesOutputModel> FromEntities(List<CategoryEntity> categories)
        {
            var result = new List<ListCategoriesOutputModel>();

            categories.ForEach(x =>
                {
                    result.Add(FromEntity(x));
                }
            );

            return result;
        }
    }
}
