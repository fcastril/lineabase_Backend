using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class PackageMigrationValidator : AbstractValidator<PackageMigrationDto>
    {
		private readonly IPackageMigrationRepository _packageMigrationRepository;

        public PackageMigrationValidator(IPackageMigrationRepository packageMigrationRepository)
        {
            _packageMigrationRepository = packageMigrationRepository;
        }
    }
}