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
    public class DeleteRepositoryMigrationsByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>> implementationMock;
        IRequestHandler<DeleteRepositoryMigrationByDiscoveryIdAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>>();
            handler = new DeleteRepositoryMigrationByDiscoveryIdAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            DeleteRepositoryMigrationByDiscoveryIdAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteAllModels(It.IsAny<Expression<Func<RepositoryMigration, bool>>>())).ReturnsAsync(true);
            bool result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
