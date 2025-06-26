using CoreGenerator;
using CoreGenerator.Dtos;
using CoreGenerator.LogicSqlServer;
using Microsoft.AspNetCore.Mvc;
using SqliteConnector.Entities;

namespace Api.Generator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : HandlerController<ProjectEnt,ProjectDto>
    {
        public ProjectController(IProjectBL projectBL)
            : base(projectBL)
        {

        }
    }
}
