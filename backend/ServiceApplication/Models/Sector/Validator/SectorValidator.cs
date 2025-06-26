
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class SectorValidator : AbstractValidator<SectorDto>
    {
		private readonly ISectorRepository _sectorRepository;
        public SectorValidator(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
            
        }
        
    }
}