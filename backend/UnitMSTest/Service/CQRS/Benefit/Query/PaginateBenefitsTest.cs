using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Util.Common;

namespace UnitMSTest.Service
{
    [TestClass]
    public class PaginateBenefitsTest
    {
        Mock<IBaseServiceApplication<Benefit, BenefitDto>> implementationMock;
        IRequestHandler<PaginateBenefitAsyncQuery, Paginate<BenefitDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<Benefit, BenefitDto>>();
            handler = new PaginateBenefitAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            CancellationToken cancellationToken = new();
            PaginateBenefitAsyncQuery request = new(new(), "12345");
            Paginate<BenefitDto> paginate = new()
            {
                Count = 1,
                Page = 1,
                Data = new List<BenefitDto>() { new() { Id = "1", Category = "Security" } }
            };

            implementationMock.Setup(x => x.Paginate(It.IsAny<Paginate<BenefitDto>>())).ReturnsAsync(paginate);
            Paginate<BenefitDto> result = await handler.Handle(request, cancellationToken);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
