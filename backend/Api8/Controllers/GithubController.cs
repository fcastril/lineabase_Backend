using Api.Base;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApplication.CQRS;
using Utilidades;

namespace Api8.Controllers.Base
{

    [AllowAnonymous]
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]
    public class GithubController : HandlerBaseLiteController<GithubAppRequest>
    {
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="mediator"></param>
        public GithubController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get token for github app
        /// </summary>
        /// <param name="githubAppRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getTokenGithubApp")]
        public async Task<IActionResult> GetTokenGithubApp(GithubAppRequest githubAppRequest)
          => this.HandlerResponse(await _mediator.Send(new GetGithubAppTokenQuery(githubAppRequest)));

        /// <summary>
        /// Get refresh token for github app
        /// </summary>
        /// <param name="githubAppRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getRefreshTokenGithubApp")]
        public async Task<IActionResult> GetRefreshTokenGithubApp(GithubAppRequest githubAppRequest)
          => this.HandlerResponse(await _mediator.Send(new GetGithubAppRefreshTokenQuery(githubAppRequest)));


        /// <summary>
        /// Get email from github app
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("getEmail")]
        public async Task<IActionResult> GetEmail([FromBody] GithubAppBaseRequest githubAppBaseRequest)
        {
            List<GithubAppEmailResponse> emails = await _mediator.Send(new GetGithubAppEmailsQuery(githubAppBaseRequest.Token));
            string email = emails.FirstOrDefault()!.Email;
            return this.HandlerResponse(email);

        }
    }
}