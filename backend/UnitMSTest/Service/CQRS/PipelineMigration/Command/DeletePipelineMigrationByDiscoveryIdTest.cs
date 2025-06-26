using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class DeletePipelineMigrationsByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<PipelineMigration, PipelineMigrationDto>> implementationMock;
        IRequestHandler<DeletePipelineMigrationByDiscoveryIdAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<PipelineMigration, PipelineMigrationDto>>();
            handler = new DeletePipelineMigrationByDiscoveryIdAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            DeletePipelineMigrationByDiscoveryIdAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteAllModels(It.IsAny<Expression<Func<PipelineMigration, bool>>>())).ReturnsAsync(true);
            bool result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
