using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.Validator
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
		private readonly ICustomerRepository _customerRepository;

        public CustomerValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MustAsync(ValidateEmailExist).WithMessage("Email is already registered.");
        }
        
        private async Task<bool> ValidateEmailExist(string email, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FirstOrDefautlModelBy(x => x.Email == email);

            return customer == null;
        } 
    }
}