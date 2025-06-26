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
    public class DeleteEstimationOverviewByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<EstimationOverview, EstimationOverviewDto>> implementationMock;
        IRequestHandler<DeleteEstimationOverviewByDiscoveryIdAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<EstimationOverview, EstimationOverviewDto>>();
            handler = new DeleteEstimationOverviewByDiscoveryIdAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DeleteEstimationOverviewByDiscoveryIdAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteAllModels(It.IsAny<Expression<Func<EstimationOverview, bool>>>())).ReturnsAsync(true);
            bool result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
