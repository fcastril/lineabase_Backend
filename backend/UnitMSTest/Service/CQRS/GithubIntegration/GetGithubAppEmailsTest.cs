using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication.CQRS;
using ServiceApplication.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class GetGithubAppEmailsTest
    {
        Mock<IGithubService> implementationMock;
        IRequestHandler<GetGithubAppEmailsQuery, List<GithubAppEmailResponse>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IGithubService>();
            handler = new GetGithubAppEmailsQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            List<GithubAppEmailResponse> response = new()
            {
                new() { Email = "email@email.com", Primary = true },
                new() { Email = "swo@email.com", Primary = false }
            };
            GetGithubAppEmailsQuery request = new("12345");

            implementationMock.Setup(x => x.GetGithubAppEmail(It.IsAny<string>())).ReturnsAsync(response);
            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("email@email.com", result[0].Email);
        }
    }
}
