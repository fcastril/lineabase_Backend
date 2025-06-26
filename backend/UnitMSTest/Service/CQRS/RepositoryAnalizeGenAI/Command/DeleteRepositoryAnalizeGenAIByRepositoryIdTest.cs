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
    public class DeleteRepositoryAnalizeGenAIsByRepositoryIdTest
    {
        Mock<IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>> implementationMock;
        IRequestHandler<DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>>();
            handler = new DeleteRepositoryAnalizeGenAIByRepositoryIdAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteAllModels(It.IsAny<Expression<Func<RepositoryAnalizeGenAI, bool>>>())).ReturnsAsync(true);
            bool result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
