using System.Linq.Expressions;
using AutoMapper;
using SqliteConnector.Common;

namespace CoreGenerator;

public class BaseBL<ENT,DTO> 
where ENT : class, new()
where DTO : class, new()
{
    public IBaseRepository<ENT> Context { get; set;}
    protected IMapper _mapper;
    public BaseBL(IBaseRepository<ENT> context, IMapper mapper)
    {
        _mapper = mapper;
        Context = context;
    }

    public async Task Create(DTO ent)
    {
        await Context.CreateModel(_mapper.Map<ENT>(ent));
    }

    public async Task<List<DTO>> ToListWhere(Expression<Func<ENT, bool>> expression)
    {
        var rest = await Context.ToListModelBy(expression);
        return _mapper.Map<List<DTO>>(rest);
    }

    public async Task<List<DTO>> ToList()
    {
        var rest = await Context.TolistModel();
        return _mapper.Map<List<DTO>>(rest);
    }

    public async Task Update(DTO dto)
    {
        await Context.UpdateModel(_mapper.Map<ENT>(dto));
    }

    public async Task Delete(int id)
    {
        await Context.DeleteModel(id);
    }

    public async Task<DTO> FirstOrDefault(Expression<Func<ENT, bool>> expression)
    {
        var rest = await Context.FirstOrDefautlModelBy(expression);
        return _mapper.Map<DTO>(rest);
    }
}
