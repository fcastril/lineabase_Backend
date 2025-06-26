
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class DiscoveryValidator : AbstractValidator<DiscoveryDto>
    {
		private readonly IDiscoveryRepository _discoveryRepository;
        public DiscoveryValidator(IDiscoveryRepository discoveryRepository)
        {
            _discoveryRepository = discoveryRepository;
            
        }
        
    }
}