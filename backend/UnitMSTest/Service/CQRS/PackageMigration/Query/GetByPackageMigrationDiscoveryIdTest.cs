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
    public class GetByPackageMigrationDiscoveryIdTest
    {
        Mock<IBaseServiceApplication<PackageMigration, PackageMigrationDto>> implementationMock;
        IRequestHandler<GetByPackageMigrationDiscoveryIdAsyncQuery, List<PackageMigrationDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<PackageMigration, PackageMigrationDto>>();
            handler = new GetByPackageMigrationDiscoveryIdAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            GetByPackageMigrationDiscoveryIdAsyncQuery request = new("12345");
            List<PackageMigrationDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345" }
            };

            implementationMock.Setup(x => x.TolistDtoBy(It.IsAny<Expression<Func<PackageMigration, bool>>>())).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
