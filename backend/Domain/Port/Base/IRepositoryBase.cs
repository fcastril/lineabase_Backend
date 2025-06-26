using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.Common;

namespace Domain.Port
{
    public interface IRepositoryBase<T>
        where T : class, new()
    {

        Task<List<T>> TolistModel();

        Task<bool> DeleteModel(string property, string value);
        Task<bool> DeleteModels(Expression<Func<T, bool>> expression);

        Task<bool> DeleteModels(string property, string value);

        Task<T> CreateModel(T entity);

        Task CreateModels(List<T> entity);

        Task<T> UpdateModel(T entity);

        Task<List<T>> ToListModelBy(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefautlModelBy(Expression<Func<T, bool>> expression);

        Task<Paginate<T>> Paginate(int pagina, int tamaño);

        Task<Paginate<T>> Paginate(Paginate<T> paginadoDto);

        Task<long> Count();

        Task<long> Count(Expression<Func<T, bool>> expression);

    }
}
