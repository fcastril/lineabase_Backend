using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;
using System.Text.RegularExpressions;

namespace ServiceApplication.Validator
{
    public class ConnectToolValidator : AbstractValidator<ConnectToolDto>
    {
        private readonly IConnectToolRepository _connectToolRepository;

        public ConnectToolValidator(IConnectToolRepository connectToolRepository)
        {
            _connectToolRepository = connectToolRepository;

            RuleFor(c => c.Organization)
                .NotEmpty()
                .WithErrorCode("400");

            RuleFor(c => c.PAT)
                .NotEmpty()
                .Length(84)
                .Must(EsTokenValido).WithMessage("Please enter a valid personal access token.")
                .WithErrorCode("400");
        }

        private bool EsTokenValido(string token)
        {
            return Regex.IsMatch(token, @"^[A-Za-z0-9]{52}.*$");
        }
    }
}
