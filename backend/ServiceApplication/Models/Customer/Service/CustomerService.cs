
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class CustomerService : BaseServiceApplication<Customer,CustomerDto>, ICustomerService
    {

        public CustomerService(ICustomerRepository customerRepository, IBaseServiceApplication<Sector, SectorDto> sectorService): base(customerRepository)
        {
            
            
            CreateMapperExpresion<Customer, CustomerDto>(cnf =>
            {
                CustomerMapper.Expresion(cnf, sectorService);
            });
        }
        
    }
}