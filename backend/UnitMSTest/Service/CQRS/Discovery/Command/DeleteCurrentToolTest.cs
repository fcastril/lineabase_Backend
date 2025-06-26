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
    public class DeleteCurrentToolTest
    {
        Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>> implementationMock;
        IRequestHandler<DeleteCurrentToolAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<CurrentTool, CurrentToolDto>>();
            handler = new DeleteCurrentToolAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DeleteCurrentToolAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteModel(It.IsAny<string>())).ReturnsAsync(true);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
