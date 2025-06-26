using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class SecurityMigrationValidator : AbstractValidator<SecurityMigrationDto>
    {
		private readonly ISecurityMigrationRepository _securityMigrationRepository;

        public SecurityMigrationValidator(ISecurityMigrationRepository securityMigrationRepository)
        {
            _securityMigrationRepository = securityMigrationRepository;
        }
    }
}