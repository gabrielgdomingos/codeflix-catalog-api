using FC.CodeFlix.Catalog.Domain.Common;
using FC.CodeFlix.Catalog.Domain.Validations;

namespace FC.CodeFlix.Catalog.Domain.Entities.Categories
{
    public class CategoryEntity
        : AggregateRoot
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public CategoryEntity(string name, string description)
            : this(name, description, true) { }

        public CategoryEntity(string name, string description, bool isActive)
            : base()
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            validate();
        }

        private void validate()
        {
            DomainValidation.NotNullOrEmpty(nameof(Name), Name);
            DomainValidation.MinLength(nameof(Name), Name, 3);
            DomainValidation.MaxLength(nameof(Name), Name, 255);

            DomainValidation.NotNull(nameof(Description), Description);
            DomainValidation.MaxLength(nameof(Description), Description, 10000);
        }

        public void Activate()
        {
            IsActive = true;
            validate();
        }

        public void Deactivate()
        {
            IsActive = false;
            validate();
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            validate();
        }
    }
}
