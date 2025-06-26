using CoreGenerator;
using CoreGenerator.Dtos;
using CoreGenerator.LogicSqlServer;
using Microsoft.AspNetCore.Mvc;
using SqliteConnector.Entities;

namespace Api.Generator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchitectureController : HandlerController<ArchitectureEnt,ArchitectureDto>
    {
        public ArchitectureController(IArchitectureBL architectureBL)
        :base(architectureBL)
        {
        }
    }
}
