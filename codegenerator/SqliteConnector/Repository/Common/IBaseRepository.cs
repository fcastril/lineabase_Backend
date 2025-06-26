using System.Linq.Expressions;

namespace SqliteConnector.Common;

public interface IBaseRepository <T>
        where T : class, new()
{
        Task<T> GetById(int id);
        Task<T> GetById(string id);

        Task<List<T>> TolistModel();

        Task<bool> DeleteModel(int id);
        Task<bool> DeleteModel(string id);

        Task<T> CreateModel(T entity);

        Task CreateModels(List<T> entity);

        Task<T> UpdateModel(T entity);

        Task<List<T>> ToListModelBy(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefautlModelBy(Expression<Func<T, bool>> expression);

        Task<bool> Exist(Expression<Func<T, bool>> expression);
}
