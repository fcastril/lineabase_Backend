using System;
using Domain.Port;
using System.Linq;
using Util.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Infrastructure
{
    public class RepositoryBase<T> : IRepositoryBase<T>
          where T : class, new()
    {
        public IMainContextCosmos MainContext { get; set; }

        protected IMongoCollection<T> Coleccion;
        public RepositoryBase(IMainContextCosmos mainContext)
        {
            MainContext = mainContext;
            Coleccion = MainContext.GetCollection<T>(typeof(T).Name);
        }

        public async Task<List<T>> TolistModel()
        {
            var list = await Coleccion.Find(x => true).ToListAsync();
            return list ?? default;
        }

        public async Task<bool> DeleteModel(string property, string value)
        {
            await Coleccion.DeleteOneAsync(Builders<T>.Filter.Eq(property, value));
            return true;
        }

        public async Task<bool> DeleteModels(string property, string value)
        {
            await Coleccion.DeleteManyAsync(Builders<T>.Filter.Eq(property, value));
            return true;
        }

        public async Task<bool> DeleteModels(Expression<Func<T, bool>> expression)
        {
            await Coleccion.DeleteManyAsync(expression);
            return true;
        }

        public async Task<T> CreateModel(T entity)
        {
            await Coleccion.InsertOneAsync(entity);
            return entity;
        }

        public async Task CreateModels(List<T> entity)
        {
            await Coleccion.InsertManyAsync(entity);
        }

        public async Task<T> UpdateModel(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity = SetPropertyValue("DateLastUpdate", entity, DateTime.UtcNow);
            string id = GetPropertyValue("Id", entity);

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("The entity must have a valid Id property.");
            }

            await Coleccion.FindOneAndReplaceAsync(Builders<T>.Filter.Eq("Id", id), entity);
            return entity;
        }

        public async Task<List<T>> ToListModelBy(Expression<Func<T, bool>> expression)
        {
            var list = await Coleccion.Find(expression).ToListAsync();
            return list ?? default;
        }

        public async Task<T> FirstOrDefautlModelBy(Expression<Func<T, bool>> expression)
        {
            var entity = await Coleccion.Find(expression).SingleOrDefaultAsync();
            return entity ?? default;
        }

        public async Task<Paginate<T>> Paginate(int pagina, int tamaño)
        {
            return await Paginator<T>.Paginate(Coleccion.AsQueryable(), pagina, tamaño);
        }

        public async Task<Paginate<T>> Paginate(Paginate<T> paginadoDto)
        {
            IQueryable<T> Listabase;
            if (paginadoDto.FiltersPaginate != null && paginadoDto.FiltersPaginate.Count > 0)
            {
                FilterDefinition<T> filtrado = this.ConfigurateFilters(paginadoDto.FiltersPaginate, paginadoDto.Operator);

                Listabase = this.Coleccion.Find(filtrado).ToList().AsQueryable();
            }
            else
            {
                Listabase = Coleccion.AsQueryable();
            }

            return await Paginator<T>.Paginate(Listabase, paginadoDto.Page, paginadoDto.Count);
        }

        public async Task<long> Count()
        {
            return await Count(null);
        }

        public async Task<long> Count(Expression<Func<T, bool>> expression)
        {
            var filter = expression != null ? expression : Builders<T>.Filter.Empty;
            return await this.Coleccion.CountDocumentsAsync(filter);
        }

        private FilterDefinition<T> ConfigurateFilters(List<FilterPaginate> filtros, LogicalOperators operador)
        {
            FilterDefinition<T> filter = null;
            List<FilterDefinition<T>> filters = new List<FilterDefinition<T>>();

            foreach (FilterPaginate filterValue in filtros)
            {
                filters.Add(GetFilter(filterValue.Property, filterValue.Value));
            }

            if (filters.Count == 1)
            {
                filter = GetFilter(filtros.FirstOrDefault().Property, filtros.FirstOrDefault().Value);
            }
            else if (filters.Count > 0)
            {
                if (operador == LogicalOperators.And)
                {
                    filter = Builders<T>.Filter.And(filters);
                }
                else if (operador == LogicalOperators.Or)
                {
                    filter = Builders<T>.Filter.Or(filters);
                }
            }
            return filter;
        }

        private FilterDefinition<T> GetFilter(string filterProperty, string filterValue)
        {
            filterValue = filterValue.ToLower();
            string[] formats = { "yyyy-MM-dd" };
            FilterDefinition<T> filter;

            if (DateTime.TryParseExact(filterValue, formats, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateTime dateTime))
            {
                filter = Builders<T>.Filter.And(
                Builders<T>.Filter.Gte(filterProperty, dateTime.Date),
                Builders<T>.Filter.Lte(filterProperty, dateTime.AddDays(1).Date)
                );
            }
            else if (filterValue == "false" || filterValue == "true")
            {
                var nameFieldDefinition = new StringFieldDefinition<T, bool>(filterProperty);
                filter = Builders<T>.Filter.Eq(nameFieldDefinition, Convert.ToBoolean(filterValue));
            }
            else
            {
                var nameFieldDefinition = new StringFieldDefinition<T, string>(filterProperty);
                filter = Builders<T>.Filter.Regex(nameFieldDefinition, new BsonRegularExpression($"(?i).*{filterValue}.*"));
            }

            return filter;
        }

        private string GetPropertyValue(string NameProperty, T obj)
        {
            var property = obj.GetType().GetProperty(NameProperty);
            if (property == null)
            {
                throw new ArgumentException($"The property '{NameProperty}' does not exist on type '{typeof(T).Name}'.");
            }

            var value = property.GetValue(obj, null);
            return value?.ToString();
        }

        private T SetPropertyValue<v>(string NameProperty, T obj, v value)
        {
            var property = obj.GetType().GetProperty(NameProperty);
            if (property == null)
            {
                throw new ArgumentException($"The property '{NameProperty}' does not exist on type '{typeof(T).Name}'.");
            }

            property.SetValue(obj, value);
            return obj;
        }
    }
}
