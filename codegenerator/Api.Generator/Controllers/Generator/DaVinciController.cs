using CoreGenerator;
using CoreGenerator.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Api.Generator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaVinciController : HandlerLiteController
    {
        private readonly ICoreGenerator _coreGenerator;

        public DaVinciController(ICoreGenerator coreGenerator)
        {
            _coreGenerator = coreGenerator;
        }
        [HttpGet("/{entity}")]
        public async Task<IActionResult> Get(Entity entity)
        {
            try
            {
                await _coreGenerator.GenerateServicesApplication(entity);
                await _coreGenerator.GenerateInfraestructure(entity);
                await _coreGenerator.GenerateApi(entity);
                return Ok("Ready");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("go")]
        public async Task<IActionResult> GodaVinci(DaVinciDto daVinciDto)
        {
            try
            {
                await _coreGenerator.GodaVinci(daVinciDto);
                return HandlerResponse("DaVinci is writing, Okeeey?");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
