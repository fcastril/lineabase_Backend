using System.Linq.Expressions;

namespace CoreGenerator;

 public interface IBaseBL<ENT,DTO>
        where ENT : class, new()
        where DTO : class, new()
{
     Task Create(DTO ent);
     Task<DTO> FirstOrDefault(Expression<Func<ENT, bool>> expression);
     Task<List<DTO>> ToList();
     Task Update(DTO ent);
     Task Delete(int id);
}