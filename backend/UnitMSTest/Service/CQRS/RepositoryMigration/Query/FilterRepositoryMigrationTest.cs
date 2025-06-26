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
    public class FilterRepositoryMigrationTest
    {
        Mock<IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>> implementationMock;
        IRequestHandler<FilterStatusLastUpdateRepositoryMigrationQuery, List<RepositoryMigrationDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>>();
            handler = new FilterStatusLastUpdateRepositoryMigrationQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            FilterStatusLastUpdateRepositoryMigrationQuery request = new(true, DateTime.Now);
            List<RepositoryMigrationDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345" }
            };

            implementationMock.Setup(x => x.ToListModelBy(It.IsAny<Expression<Func<RepositoryMigration, bool>>>())).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
