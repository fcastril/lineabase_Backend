using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class LoginTest
    {
        Mock<ISecurityService> implementationMock;
        IRequestHandler<LoginAsyncQuery, Login> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<ISecurityService>();
            handler = new LoginAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            Login request = new() { UserName = "user", Password = "password" };
            Login response = new() { UserName = "user", Password = "password", Token = "Token123" };
            LoginAsyncQuery query = new(request);

            implementationMock.Setup(x => x.Login(It.IsAny<Login>())).ReturnsAsync(response);
            var result = await handler.Handle(query, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("Token123", result.Token);
        }
    }
}
