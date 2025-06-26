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
    public class GetByPipelineMigrationDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<PipelineMigration, PipelineMigrationDto>> implementationMock;
        IRequestHandler<GetByPipelineMigrationDiscoveryIdAsyncQuery, List<PipelineMigrationDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<PipelineMigration, PipelineMigrationDto>>();
            handler = new GetByPipelineMigrationDiscoveryIdAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            GetByPipelineMigrationDiscoveryIdAsyncQuery request = new("12345");
            List<PipelineMigrationDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345" }
            };

            implementationMock.Setup(x => x.TolistDtoBy(It.IsAny<Expression<Func<PipelineMigration, bool>>>())).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
