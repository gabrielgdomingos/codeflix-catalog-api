﻿using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Repositories;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;
using System;
using System.Collections.Generic;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Categories.Common
{
    public abstract class CategoryUseCaseTestFixtureBase
        : TestFixtureBase
    {
        public Mock<ICategoryRepository> GetRepositoryMock()
            => new();

        public Mock<IUnitOfWork> GetUnitOfWorkMock()
           => new();

        public string GetValidCategoryName()
        {
            var name = "ab";

            while (name.Length < 3)
                name = Faker.Commerce.Categories(1)[0];

            if (name.Length > 255)
                name = name[..255];

            return name;
        }

        public string GetValidCategoryDescription()
        {
            var description = Faker.Commerce.ProductDescription();

            if (description.Length > 10000)
                description = description[..10000];

            return description;
        }

        public bool GetRandomBoolean()
            => new Random().NextDouble() < 0.5;

        public CategoryEntity GetValidCategory()
          => new(
              GetValidCategoryName(),
              GetValidCategoryDescription(),
              GetRandomBoolean()
          );

        public List<CategoryEntity> GetValidCategories(int length = 10)
        {
            var categories = new List<CategoryEntity>();

            for (int i = 0; i < length; i++)
                categories.Add(GetValidCategory());

            return categories;
        }
    }
}
