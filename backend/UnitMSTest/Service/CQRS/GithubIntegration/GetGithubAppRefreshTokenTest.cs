using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication.CQRS;
using ServiceApplication.Interface;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class GetGithubAppRefreshTokenTest
    {
        Mock<IGithubService> implementationMock;
        IRequestHandler<GetGithubAppRefreshTokenQuery, GithubAppResponse> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IGithubService>();
            handler = new GetGithubAppRefreshTokenQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            GithubAppRequest req = new() { ClientId = "1234", ClientSecret = "1234" };
            GithubAppResponse res = new() { TokenType = "Bearer", AccessToken = "12345" };
            GetGithubAppRefreshTokenQuery request = new(req);

            implementationMock.Setup(x => x.GetGithubAppRefreshToken(It.IsAny<GithubAppRequest>())).ReturnsAsync(res);
            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("12345", result.AccessToken);
        }
    }
}