
using AutoMapper;
using Domain.Entities;
using ServiceApplication.Base;
using ServiceApplication.Dto;

namespace ServiceApplication.Mapper
{
	public static class CustomerMapper
	{

		public static void Expresion(IMapperConfigurationExpression cnf, IBaseServiceApplication<Sector, SectorDto> sectorService)
		{

			SectorMapper.Expresion(cnf);

			cnf.CreateMap<CustomerDto, Customer>()
				.ConstructUsing(src => src != null ? new Customer(
					src.Name,
					src.Email,
					src.Developers,
					sectorService.MapToENT<Sector, SectorDto>(src.Sector),
					src.MarketingEmails,
					src.NewsUpdate,
					src.ProductionProcess,
					src.Status) : null);
			cnf.CreateMap<Customer, CustomerDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Developers, opt => opt.MapFrom(src => src.Developers))
				.ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector))
				.ForMember(dest => dest.MarketingEmails, opt => opt.MapFrom(src => src.MarketingEmails))
				.ForMember(dest => dest.NewsUpdate, opt => opt.MapFrom(src => src.NewsUpdate))
				.ForMember(dest => dest.ProductionProcess, opt => opt.MapFrom(src => src.ProductionProcess))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
		}
	}
}