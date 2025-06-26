using Api.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication;
using ServiceApplication.Dto;
using Utilidades;

namespace Api8.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class OverviewMigrationController : HandlerBaseLiteController<OverviewMigrationDto>
    {
        private readonly IOverviewMigrationService _overviewMigrationService;

        public OverviewMigrationController(IOverviewMigrationService overviewMigrationService, IMediator mediator) : base(mediator)
        {
            _overviewMigrationService = overviewMigrationService;
        }

        [HttpGet("GetById/{discoveryId}")]
        public async Task<IActionResult> GetById(string discoveryId)
        {
            List<OverviewMigrationDto> overview = await _overviewMigrationService.GetOverview(discoveryId);
            return HandlerResponse(overview);
        }
    }
}
