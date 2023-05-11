using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;
using System.Collections.Generic;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class UserMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf, IRolService service)
        {

            RolMapper.Expresion(cnf);


            cnf.CreateMap<UserDto, User>()
                .ConstructUsing(s => s != null ? new User
                    (s.Id, s.UserName, s.Email, s.Name, s.Password, s.DateCreation, s.Status, s.DateLastUpdate, service.MapToENT<Rol, RolDto>(s.Rol)) : null);

            cnf.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(x => string.Empty))
                ;

        }

       
    }

}

