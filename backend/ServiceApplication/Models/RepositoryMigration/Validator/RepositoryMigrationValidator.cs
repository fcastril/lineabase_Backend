using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class RepositoryMigrationValidator : AbstractValidator<RepositoryMigrationDto>
    {
		private readonly IRepositoryMigrationRepository _repositoryMigrationRepository;

        public RepositoryMigrationValidator(IRepositoryMigrationRepository repositoryMigrationRepository)
        {
            _repositoryMigrationRepository = repositoryMigrationRepository;
        }
    }
}