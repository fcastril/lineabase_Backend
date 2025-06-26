
using Domain.Entities;
using Domain.Port;

namespace Infrastructure.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(IMainContextCosmos mainContext): base(mainContext)
        {
            
            
        }
        
    }
}