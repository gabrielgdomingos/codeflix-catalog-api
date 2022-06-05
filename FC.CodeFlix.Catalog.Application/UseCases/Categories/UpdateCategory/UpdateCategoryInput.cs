﻿using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryInput
        : IRequest<UpdateCategoryOutput>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public UpdateCategoryInput(Guid id, string name, string description, bool isActive)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
        }
    }
}
