using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Domain.Repositories;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryUseCase
        : IRequestHandler<UpdateCategoryInput, UpdateCategoryOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryUseCase(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<UpdateCategoryOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(request.Id, cancellationToken);

            if (category == null)
                throw new NotFoundException($"Category '{request.Id}' not found");

            category.Update(request.Name, request.Description);

            if (request.IsActive != category.IsActive)
            {
                if (request.IsActive)
                    category.Activate();
                else
                    category.Deactivate();
            }

            await _categoryRepository.UpdateAsync(category, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return UpdateCategoryOutput.FromEntity(category);
        }
    }
}
