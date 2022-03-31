using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.Entities.Categories
{
    public class CategoryEntity
    {
        public Guid Id { get; private set; }        

        public string Name { get; private set; }

        public string Description { get; private set; }        

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public CategoryEntity(string name, string description, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;
            validate();
        }

        public void validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null");

            if (Name.Length < 3)
                throw new EntityValidationException($"{nameof(Name)} should be at leats 3 characters long");

            if (Name.Length > 255)
                throw new EntityValidationException($"{nameof(Name)} should be less or equal 255 characters long");

            if (Description == null)
                throw new EntityValidationException($"{nameof(Description)} should not be null");

            if (Description.Length > 10000)
                throw new EntityValidationException($"{nameof(Description)} should be less or equal 10.000 characters long");
        }
    }
}
