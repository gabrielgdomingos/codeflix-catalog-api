using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Domain.Entities.Categories;
using FC.CodeFlix.Catalog.Domain.Repositories;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryUseCase
        : IRequestHandler<CreateCategoryInput, CreateCategoryOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryUseCase(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryOutput> Handle(
            CreateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var category = new CategoryEntity(
                input.Name,
                input.Description,
                input.IsActive
            );

            await _categoryRepository.AddAsync(category, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            var output = new CreateCategoryOutput(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt
            );

            return output;
        }
    }
}
