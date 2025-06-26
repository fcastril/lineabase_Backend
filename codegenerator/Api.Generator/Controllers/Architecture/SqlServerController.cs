using CoreGenerator;
using CoreGenerator.LogicSqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Generator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SqlServerController : HandlerLiteController
    {
        private readonly ISqlServerBL _sqlServerBL;

        public SqlServerController(ISqlServerBL sqlServerBL)
        {
            _sqlServerBL = sqlServerBL;
        }
        [HttpGet("tables/{codeProject}")]
        public async Task<IActionResult> Get(string codeProject)
        {
            return HandlerResponse(await _sqlServerBL.GetTablesAvailable(codeProject)); 
        }
    }
}
