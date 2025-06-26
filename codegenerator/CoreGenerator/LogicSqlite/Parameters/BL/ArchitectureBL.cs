using SqliteConnector.Entities;
using SqliteConnector;
using AutoMapper;
using CoreGenerator.Dtos;
using System.Linq.Expressions;

namespace CoreGenerator;

public class ArchitectureBL:BaseBL<ArchitectureEnt,ArchitectureDto>,IArchitectureBL
{
    public ArchitectureBL(IArchitectureRepository baseRepository,IMapper mapper) 
    : base(baseRepository,mapper)
    {

    }
}
