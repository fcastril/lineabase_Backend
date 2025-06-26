
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
    public static class CategoryMapper
    {
        
        public static void Expresion (IMapperConfigurationExpression cnf)
        {
            cnf.CreateMap<CategoryDto, Category>()
				.ConstructUsing(src => src != null ? new Category(src.Description, src.Status): null);
			cnf.CreateMap<Category, CategoryDto>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}