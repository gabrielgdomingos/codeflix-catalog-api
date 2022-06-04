using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCases.Categories.DeleteCategory
{
    public class DeleteCategoryInputValidator
        : AbstractValidator<DeleteCategoryInput>
    {
        public DeleteCategoryInputValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}
