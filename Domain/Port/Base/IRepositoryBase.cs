using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Common;
using Util.Common;

namespace Domain.Port
{
    public interface IRepositoryBase<T>
        where T : class, new()
    {

        Task<T> GetById(Guid id);

        Task<List<T>> TolistModel();

        Task<bool> DeleteModel(Guid id);

        Task<T> CreateModel(T entity);

        Task CreateModels(List<T> entity);

        Task<T> UpdateModel(T entity);

        Task<List<T>> ToListModelBy(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefautlModelBy(Expression<Func<T, bool>> expression);

        Task<Paginate<T>> Paginate(int page, int lenght);

        Task<Paginate<T>> Paginate(Paginate<T> paginateDto);

    }
}