using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class DeleteProblemDetailsTest
    {
        Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>> implementationMock;
        IRequestHandler<DeleteProblemDetailsAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ProblemDetails, ProblemDetailsDto>>();
            handler = new DeleteProblemDetailsAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DeleteProblemDetailsAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteModel(It.IsAny<string>())).ReturnsAsync(true);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
