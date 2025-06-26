
using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class AreaValidator : AbstractValidator<AreaDto>
    {
		private readonly IAreaRepository _areaRepository;
        public AreaValidator(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
            
        }
        
    }
}