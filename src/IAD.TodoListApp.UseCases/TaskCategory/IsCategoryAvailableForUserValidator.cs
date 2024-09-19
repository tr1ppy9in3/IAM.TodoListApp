using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace IAD.TodoListApp.UseCases.TaskCategory;

public class IsCategoryAvailableForUserValidator : AbstractValidator<long>
{
    private readonly ITaskCategoryRepository _categoryRepository;

    public IsCategoryAvailableForUserValidator(ITaskCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(categoryId => categoryId)
            .MustAsync(async (categoryId, context) =>
            {
                if (categoryId == default)
                    return true;

                var userId = (long?)ValidatorOptions.Global?.GetValidationContext()?.ParentContext?.CustomState;
                if (userId == null)
                {
                    return false; 
                }

                return await _categoryRepository.IsTaskCategoryAvailable(categoryId, userId.Value);
            })
            .WithMessage("Category is not available for this user.");
    }
}
