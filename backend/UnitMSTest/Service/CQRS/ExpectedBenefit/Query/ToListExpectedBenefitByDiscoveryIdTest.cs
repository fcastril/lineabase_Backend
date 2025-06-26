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
    public class ToListExpectedBenefitByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto>> implementationMock;
        IRequestHandler<ToListExpectedBenefitByDiscoveryIdAsyncQuery, List<ExpectedBenefitDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto>>();
            handler = new ToListExpectedBenefitByDiscoveryIdAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            ToListExpectedBenefitByDiscoveryIdAsyncQuery request = new("12345");
            List<ExpectedBenefitDto> list = new()
            {
                new() { Id = "1", Category = "Security" }
            };

            implementationMock.Setup(x => x.TolistDtoBy(It.IsAny<Expression<Func<ExpectedBenefit, bool>>>())).ReturnsAsync(list);
            List<ExpectedBenefitDto> result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
