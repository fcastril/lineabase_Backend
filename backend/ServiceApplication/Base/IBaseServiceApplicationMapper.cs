using AutoMapper;
using System;
using System.Collections.Generic;

namespace ServiceApplication.Base
{
    public interface IBaseServiceApplicationMapper<ENT, DTO>
         where ENT : class, new()
         where DTO : class, new()
    {
        List<ENT> MapLstToENT<ENT, DTO>(List<DTO> dto);
        List<DTO> MapLstToDTO<ENT, DTO>(List<ENT> entity);
        ENT MapToENT<ENT, DTO>(DTO dto);
        DTO MapToDTO<ENT, DTO>(ENT entity);
        void CreateMapperExpresion(Action<IMapperConfigurationExpression> configure);
        void CreateMapperExpresion<ENT, DTO>(Action<IMapperConfigurationExpression> configure) where ENT : class, new() where DTO : class, new();
    }
}
