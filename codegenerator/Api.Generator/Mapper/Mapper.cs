using AutoMapper;
using CoreGenerator.Dtos;
using SqliteConnector.Entities;

namespace Api
{

public class Mapper: Profile
{
     public Mapper()
        {
            CreateMap<ProjectEnt, ProjectDto>();
            CreateMap<ProjectDto, ProjectEnt>()
                .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(src => src.ConnectionString))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));

            CreateMap<ArchitectureEnt, ArchitectureDto>();
            CreateMap<ArchitectureDto, ArchitectureEnt>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Layers, opt => opt.MapFrom(src => src.Layers))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"));
        }
}
}
