using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class UserMigrationValidator : AbstractValidator<UserMigrationDto>
    {
		private readonly IUserMigrationRepository _userMigrationRepository;

        public UserMigrationValidator(IUserMigrationRepository userMigrationRepository)
        {
            _userMigrationRepository = userMigrationRepository;
        }
    }
}