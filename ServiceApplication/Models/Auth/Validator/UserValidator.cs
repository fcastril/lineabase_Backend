using System;
using Domain.Entities;
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Validator
{
    public class UserValidator: AbstractValidator<UserDto>
    {
        private readonly IUserRepository _repository;

        public UserValidator(IUserRepository repository)
        {

            _repository = repository;

            RuleFor(x => x.UserName).NotEmpty().WithErrorCode($"UserNameEmpty")
            .WithMessage(x => x.UserName)
                 .WithName(nameof(User.UserName));

            RuleFor(x => x.Password).NotEmpty().WithErrorCode($"PasswordEmpty")
           .WithMessage(x => x.Password)
                .WithName(nameof(User.Password));


            RuleFor(v => v)
                .NotNull()
                .MustAsync(async (x, cancellation) =>
                {
                    var obj = await _repository.FirstOrDefautlModelBy(UserSpecification.ExistByUsername(x.UserName, x.Id));
                    return obj == null;
                }).WithErrorCode($"Username")
                 .WithMessage(x => x.UserName.ToString())
                 .WithName(nameof(User.UserName));
        }
    }
}

