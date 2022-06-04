using FC.CodeFlix.Catalog.Domain.Repositories;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.GetCategory
{
    public class GetCategoryUseCase
        : IRequestHandler<GetCategoryInput, GetCategoryOutput>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryUseCase(ICategoryRepository categoryRepository)
            => _categoryRepository = categoryRepository;

        public async Task<GetCategoryOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(
                request.Id,
                cancellationToken
            );

            //TODO: Fazer o tratamento quando não encontrar a entidade
            return GetCategoryOutput.FromEntity(category);
        }
    }
}
