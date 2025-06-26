
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
		private readonly ICategoryRepository _categoryRepository;
        public CategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }
        
    }
}