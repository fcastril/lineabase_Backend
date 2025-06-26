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
    public class DeleteExpectedBenefitsByDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto>> implementationMock;
        IRequestHandler<DeleteExpectedBenefitsByDiscoveryIdAsyncCommand, bool> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<ExpectedBenefit, ExpectedBenefitDto>>();
            handler = new DeleteExpectedBenefitsByDiscoveryIdAsyncCommandHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            DeleteExpectedBenefitsByDiscoveryIdAsyncCommand request = new("12345");

            implementationMock.Setup(x => x.DeleteAllModels(It.IsAny<Expression<Func<ExpectedBenefit, bool>>>())).ReturnsAsync(true);
            bool result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
