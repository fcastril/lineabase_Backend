using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class RolMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {


            cnf.CreateMap<RolDto, Rol>()
                .ConstructUsing(s => s != null ? new Rol
                    (s.Id, s.Name, s.Description, s.Root, s.DateCreation, s.Status, s.DateLastUpdate) : null);

            cnf.CreateMap<Rol, RolDto>();


        }
    }
}
