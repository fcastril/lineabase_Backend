using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SqliteConnector.Common;

public class BaseRepository<T> :IBaseRepository<T>
            where T : BaseEntitySqlite, new()
{
        public ISqliteContext MainContext { get; set; }
        private DbSet<T> entity;

        public BaseRepository(ISqliteContext mainContext)
        {
            MainContext = mainContext;
            entity = MainContext.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<List<T>> TolistModel()
        {
            return await entity.ToListAsync();
        }

        public async Task<bool> DeleteModel(int id)
        {
            bool returnDelete = false;

            T obj = await GetById(id);

            if (obj != null)
            {
                entity.Remove(obj);
                returnDelete = await MainContext.SaveChangesAsync()>0;
            }

            return returnDelete;
        }

        public async Task<T> CreateModel(T obj)
        {
            await entity.AddAsync(obj);
            await MainContext.SaveChangesAsync();
            return obj;
        }

        public async Task CreateModels(List<T> listObjs)
        {
            await entity.AddRangeAsync(listObjs);
            await MainContext.SaveChangesAsync();
        }

        public async Task<T> UpdateModel(T obj)
        {
            //SetPropertyValue("DateLastUpdate", obj, DateTime.UtcNow.AddHours(-6));
            entity.Update(obj);
            int result = await MainContext.SaveChangesAsync();
            return obj;
        }

        public async Task<List<T>> ToListModelBy(Expression<Func<T, bool>> expression)
        {
            return await entity.Where(expression).ToListAsync();
        }

        public async Task<T> FirstOrDefautlModelBy(Expression<Func<T, bool>> expression)
        {
            return await entity.Where(expression).SingleOrDefaultAsync();
        }

        private string GetPropertyValue(string NameProperty, T obj)
        {
            return obj.GetType().GetProperty(NameProperty).GetValue(obj, null).ToString();
        }

        private T SetPropertyValue<v>(string NameProperty, T obj, v value)
        {
            obj.GetType().GetProperty(NameProperty).SetValue(obj, value);
            return obj;
        }
        public async Task<bool> Exist(Expression<Func<T, bool>> expression)
        {
            var result = await entity.Where(expression).ToListAsync();
            return result.Count() > 0;
        }

        public async Task<T> GetById(string id)
        {
            return await entity.FirstOrDefaultAsync(f=>f.Id.ToString()==id);
        }

        public async Task<bool> DeleteModel(string id)
        {
            bool returnDelete = false;

            T obj = await GetById(id);

            if (obj != null)
            {
                entity.Remove(obj);
                returnDelete = await MainContext.SaveChangesAsync() > 0;
            }

            return returnDelete;
        }
}
