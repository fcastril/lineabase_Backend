using Domain.Port;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Validator
{
    public class EstimationOverviewValidator : AbstractValidator<EstimationOverviewDto>
    {
        private readonly IEstimationOverviewRepository _estimationOverviewRepository;

        public EstimationOverviewValidator(IEstimationOverviewRepository estimationOverviewRepository)
        {
            _estimationOverviewRepository = estimationOverviewRepository;
        }
    }
}
