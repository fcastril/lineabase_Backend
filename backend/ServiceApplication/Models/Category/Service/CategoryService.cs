
using Domain.Entities;
using Domain.Port;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Mapper;

namespace ServiceApplication
{
    public class CategoryService : BaseServiceApplication<Category,CategoryDto>, ICategoryService
    {

        public CategoryService(ICategoryRepository categoryRepository): base(categoryRepository)
        {
            
            
            CreateMapperExpresion<Category, CategoryDto>(cnf =>
            {
                CategoryMapper.Expresion(cnf);
            });
        }
        
    }
}