﻿using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.GetCategory
{
    public class GetCategoryInput
        : IRequest<GetCategoryOutput>
    {
        public Guid Id { get; set; }

        public GetCategoryInput(Guid id)
        {
            Id = id;
        }
    }
}
