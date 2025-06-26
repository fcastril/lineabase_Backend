
using AutoMapper;
using Domain.Common;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using Util.Common;

namespace ServiceApplication.Base
{
    public partial class BaseServiceApplicationMapper <ENT, DTO >: IBaseServiceApplicationMapper<ENT, DTO>
         where ENT : class, new()
         where DTO : class, new()
    {
        private MapperConfigurationExpression configurationmapper;
        private IMapper Mapper;

        protected void CreateMapper()
        {
            MapperConfiguration cnfMapper = new(configurationmapper);
            Mapper = cnfMapper.CreateMapper();
        }
        /// <summary>
        /// Registro de reglas de mapeo de objetos de DTO o Entity
        /// </summary>
        /// <typeparam name="ENT">TSource conversion</typeparam>
        /// <typeparam name="DTO">TDestination conversion</typeparam>
        protected void CreateMapper<ENT, DTO>() where ENT : class, new() where DTO : class, new()
        {
            configurationmapper = CreateConfiguration<ENT, DTO>();
            CreateMapper();
        }

        public void CreateMapperExpresion<ENT, DTO>(Action<IMapperConfigurationExpression> configure) where ENT : class, new() where DTO : class, new()
        {
            configurationmapper = CreateConfiguration<ENT, DTO>();
            CreateMapperExpresion(configure);
        }

        public void CreateMapperExpresion(Action<IMapperConfigurationExpression> configure)
        {
            configure(configurationmapper);
            CreateMapper();
        }

        private MapperConfigurationExpression CreateConfiguration<ENT, DTO>() where ENT : class, new() where DTO : class, new()
        {
            var cnf = new MapperConfigurationExpression
            {
                AllowNullCollections = true
            };
            cnf.CreateMap<ENT, DTO>();
            cnf.CreateMap<DTO, ENT>();
            //cnf.CreateMap<List<ENT>, List<DTO>>();
            //cnf.CreateMap<List<DTO>, List<ENT>>();
            cnf.CreateMap<Paginate<ENT>, Paginate<DTO>>();
            cnf.CreateMap<Paginate<DTO>, Paginate<ENT>>();
            cnf.CreateMap<ValueObjectString, string>().ConvertUsing(n => n.Value);
            cnf.CreateMap<NameValueObject, string>().ConvertUsing(n => n.Value);
            cnf.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(n => n.UtcDateTime);
            cnf.CreateMap<DateTime, DateTimeOffset>().ConvertUsing(n => DateTime.SpecifyKind(n, DateTimeKind.Utc));
            return cnf;

        }
        /// <summary>
        /// Metodo para mapear de entidad a DTO
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DTO MapToDTO<ENT, DTO>(ENT entity)
        {
            return Mapper.Map<DTO>(entity);
        }

        /// <summary>
        /// Metodo para mapear de DTO a entidad
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ENT MapToENT<ENT, DTO>(DTO dto)
        {
            return Mapper.Map<ENT>(dto);
        }

        /// <summary>
        /// Metodo para mapear de entidad a DTO
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<DTO> MapLstToDTO<ENT, DTO>(List<ENT> entity)
        {
            return Mapper.Map<List<DTO>>(entity);
        }

        /// <summary>
        /// Metodo para mapear de DTO a entidad
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ENT> MapLstToENT<ENT, DTO>(List<DTO> dto)
        {
            var lst = Mapper.Map<List<ENT>>(dto)??default;
            return lst;
        }
    }
}
