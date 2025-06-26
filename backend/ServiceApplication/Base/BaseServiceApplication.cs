using System;
using Domain.Port;
using System.Linq;
using Util.Common;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ServiceApplication.Base
{
    public abstract partial class BaseServiceApplication<ENT, DTO> : BaseServiceApplicationMapper<ENT, DTO>, IBaseServiceApplication<ENT, DTO>
          where ENT : class, new()
          where DTO : class, new()
    {
        public IRepositoryBase<ENT> RepositoryBase { get; set; }
        public BaseServiceApplication(IRepositoryBase<ENT> repositoryBase)
        {
            RepositoryBase = repositoryBase;
        }

        /// <summary>
        /// crear una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<DTO> CreateModel(DTO dto)
        {
            await this.ValidadorNegocio(dto);
            var entity = MapToENT<ENT, DTO>(dto);
            return MapToDTO<ENT, DTO>(await RepositoryBase.CreateModel(entity));
        }

        /// <summary>
        /// crear lista de entidades
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> CreateModels(List<DTO> dtos)
        {
            var entities = MapLstToENT<ENT, DTO>(dtos);
            await RepositoryBase.CreateModels(entities);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Lista de todos los registros
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<DTO>> TolistModel()
        {
            return MapLstToDTO<ENT, DTO>(await RepositoryBase.TolistModel());
        }

        public virtual async Task<List<DTO>> TolistDtoBy(Expression<Func<ENT, bool>> expression)
        {
            return MapLstToDTO<ENT, DTO>(await RepositoryBase.ToListModelBy(expression));
        }

        /// <summary>
        /// Eliminar modelo por un ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteModel(string id)
        {
            return await RepositoryBase.DeleteModel("Id", id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAllModels()
        {
            return await RepositoryBase.DeleteModels("Status", "Active");
        }

        public virtual async Task<bool> DeleteAllModels(Expression<Func<ENT, bool>> expression)
        {
            return await RepositoryBase.DeleteModels(expression);
        }

        /// <summary>
        /// Eliminar modelo por un ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<DTO> SearchModel(string property, string value)
        {
            var dtos = await this.TolistModel();
            var dto = dtos.FirstOrDefault(w => GetPropertyValue(property, w).ToLower().Contains(value.ToLower()));
            return dto;
        }

        /// <summary>
        /// Eliminar modelo por un ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<List<DTO>> SearchListModel(string property, string value)
        {
            var dtos = await this.TolistModel();
            var dto = dtos.Where(w => GetPropertyValue(property, w).ToLower().Contains(value.ToLower())).ToList();
            return dto;
        }

        /// <summary>
        /// Actualizar entidad generico
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<DTO> UpdateModel(DTO dto)
        {
            return MapToDTO<ENT, DTO>(await RepositoryBase.UpdateModel(MapToENT<ENT, DTO>(dto)));
        }

        /// <summary>
        /// Lista de registros basado en una condición
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<List<DTO>> ToListModelBy(Expression<Func<ENT, bool>> expression)
        {



            return MapLstToDTO<ENT, DTO>(await RepositoryBase.ToListModelBy(expression));
        }

        /// <summary>
        /// Primero por defecto por expresión
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<DTO> FirstOrDefautlModelBy(Expression<Func<ENT, bool>> expression)
        {
            return MapToDTO<ENT, DTO>(await RepositoryBase.FirstOrDefautlModelBy(expression));
        }

        /// <summary>
        /// Validacion de reglas de negocio trans
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task ValidadorNegocio(DTO dto)
        {
            if (dto is null)
            {
                throw new Exception(typeof(ENT).Name + " no puede ser nula");
            }
            await Task.FromResult(0);
        }

        /// <summary>
        /// Metodo para sincronizae datos
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> SyncData(List<DTO> entities)
        {
            await ValidateSyncData(entities);
            await CreateModels(entities);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Reglas de negocio para cada entidad a sincronizar
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task ValidateSyncData(List<DTO> entities)
        {
            foreach (var item in entities)
            {
                await ValidadorNegocio(item);
            }
        }

        public virtual async Task<Paginate<DTO>> Paginate(int pagina, int Count)
        {
            return MapToDTO<Paginate<ENT>, Paginate<DTO>>(await RepositoryBase.Paginate(pagina, Count));
        }

        public virtual async Task<Paginate<DTO>> Paginate(Paginate<DTO> paginado)
        {
            return MapToDTO<Paginate<ENT>, Paginate<DTO>>(await RepositoryBase.Paginate(MapToENT<Paginate<ENT>, Paginate<DTO>>(paginado)));
        }

        public virtual async Task<long> Count()
        {
            return await RepositoryBase.Count();
        }

        public virtual async Task<long> Count(Expression<Func<ENT, bool>> expression)
        {
            return await RepositoryBase.Count(expression);
        }

        protected string GetPropertyValue(string NameProperty, DTO obj)
        {
            return obj.GetType().GetProperty(NameProperty).GetValue(obj, null).ToString();
        }

        protected string GetPropertyValue(string NameProperty, ENT obj)
        {
            return obj.GetType().GetProperty(NameProperty).GetValue(obj, null).ToString();
        }
    }
}
