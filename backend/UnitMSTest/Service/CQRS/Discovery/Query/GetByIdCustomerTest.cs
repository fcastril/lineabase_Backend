using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class GetByIdCustomerTest
    {
        Mock<IBaseServiceApplication<Discovery, DiscoveryDto>> implementationMock;
        IRequestHandler<GetByIdCustomerAsyncQuery, DiscoveryDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<Discovery, DiscoveryDto>>();
            handler = new GetByIdCustomerAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            DiscoveryDto discoveryDto = new() { Id = "12345", Description = "description", Customer = new() { Id = "1" } };
            GetByIdCustomerAsyncQuery request = new("12345");

            implementationMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<Discovery, bool>>>())).ReturnsAsync(discoveryDto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Customer.Id);
        }
    }
}
