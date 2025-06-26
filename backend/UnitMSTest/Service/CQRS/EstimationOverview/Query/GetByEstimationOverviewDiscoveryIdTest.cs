using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class GetByEstimationOverviewDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<EstimationOverview, EstimationOverviewDto>> implementationMock;
        IRequestHandler<GetByEstimationOverviewDiscoveryIdAsyncQuery, List<EstimationOverviewDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<EstimationOverview, EstimationOverviewDto>>();
            handler = new GetByEstimationOverviewDiscoveryIdAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            GetByEstimationOverviewDiscoveryIdAsyncQuery request = new("12345");
            List<EstimationOverviewDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345", Category = "Security" }
            };

            implementationMock.Setup(x => x.TolistDtoBy(It.IsAny<Expression<Func<EstimationOverview, bool>>>())).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
