using AutoMapper;
using Domain.Entities;
using ServiceApplication.Dto;
using System.Collections.Generic;

namespace ServiceApplication.Models.Auth.Mapper
{
    public static class UserMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {

            RolMapper.Expresion(cnf);


            cnf.CreateMap<UserDto, User>()
                .ConstructUsing(s => s != null ? new User
                    (s.UserName, s.Email, s.Nombre, s.Password, setListRole(s.Roles)) : null);

            cnf.CreateMap<UserDto, User>();

        }

        private static List<Rol> setListRole(List<RolDto> listRolDto)
        {
            List<Rol> rols = new List<Rol>();
            foreach (RolDto item in listRolDto)
            {
                rols.Add(new Rol(item.Id, item.Name, item.Description, item.Root));
            }
            return rols;
        }
    }

}

