
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {

        public CustomerRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}