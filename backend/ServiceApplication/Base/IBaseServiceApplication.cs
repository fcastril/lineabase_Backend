using ServiceApplication.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.Common;

namespace ServiceApplication
{
    public interface IBaseServiceApplication<ENT, DTO> : IBaseServiceApplicationMapper<ENT, DTO>
        where ENT : class, new()
         where DTO : class, new()
    {
        Task<DTO> CreateModel(DTO dto);

        Task<bool> CreateModels(List<DTO> dtos);

        Task<List<DTO>> TolistModel();
        Task<List<DTO>> TolistDtoBy(Expression<Func<ENT, bool>> expression);

        Task<bool> DeleteModel(string id);
        Task<bool> DeleteAllModels();

        Task<bool> DeleteAllModels(Expression<Func<ENT, bool>> expression);

        Task<DTO> SearchModel(string property, string value);

        Task<List<DTO>> SearchListModel(string property, string value);

        Task<DTO> UpdateModel(DTO entity);

        Task<List<DTO>> ToListModelBy(Expression<Func<ENT, bool>> expression);

        Task<DTO> FirstOrDefautlModelBy(Expression<Func<ENT, bool>> expression);

        Task<Paginate<DTO>> Paginate(int pagina, int Count);

        Task<Paginate<DTO>> Paginate(Paginate<DTO> paginado);

        Task<long> Count();

        Task<long> Count(Expression<Func<ENT, bool>> expression);

    }
}
