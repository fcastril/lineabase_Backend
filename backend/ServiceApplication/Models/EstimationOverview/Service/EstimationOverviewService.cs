using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class EstimationOverviewService : BaseServiceApplication<EstimationOverview, EstimationOverviewDto>, IEstimationOverviewService
    {
        public EstimationOverviewService(IEstimationOverviewRepository estimationOverviewRepository) : base(estimationOverviewRepository)
        {
            CreateMapperExpresion<EstimationOverview, EstimationOverviewDto>(cnf =>
            {
                EstimationOverviewMapper.Expresion(cnf);
            });
        }
    }
}
