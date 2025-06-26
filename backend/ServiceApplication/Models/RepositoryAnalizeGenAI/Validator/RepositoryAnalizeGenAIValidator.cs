using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class RepositoryAnalizeGenAIValidator : AbstractValidator<RepositoryAnalizeGenAIDto>
    {
		private readonly IRepositoryAnalizeGenAIRepository _repositoryAnalizeGenAIRepository;

        public RepositoryAnalizeGenAIValidator(IRepositoryAnalizeGenAIRepository repositoryAnalizeGenAIRepository)
        {
            _repositoryAnalizeGenAIRepository = repositoryAnalizeGenAIRepository;
        }
    }
}