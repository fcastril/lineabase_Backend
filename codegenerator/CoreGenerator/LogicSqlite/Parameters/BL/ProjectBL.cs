using SqliteConnector.Entities;
using SqliteConnector;
using CoreGenerator.Dtos;
using AutoMapper;

namespace CoreGenerator;

public class ProjectBL:BaseBL<ProjectEnt,ProjectDto>,IProjectBL
{
    protected readonly IMapper _mapper;
    public ProjectBL(IProjectRepository baseRepository,IMapper mapper) 
    : base(baseRepository,mapper)
    {
        _mapper=mapper;
    }
}
