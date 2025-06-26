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
using System.Threading;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class ToListBenefitByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<Benefit, BenefitDto>> implementationMock;
        IRequestHandler<ToListBenefitByDiscoveryIdAsyncQuery, List<BenefitDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<Benefit, BenefitDto>>();
            handler = new ToListBenefitByDiscoveryIdAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            ToListBenefitByDiscoveryIdAsyncQuery request = new("12345");
            List<BenefitDto> list = new()
            {
                new() { Id = "1", Category = "Security" }
            };

            implementationMock.Setup(x => x.TolistDtoBy(It.IsAny<Expression<Func<Benefit, bool>>>())).ReturnsAsync(list);
            List<BenefitDto> result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
