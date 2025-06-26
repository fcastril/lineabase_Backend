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
    public class FilterRoleActivityUserMigrationTest
    {
        Mock<IBaseServiceApplication<UserMigration, UserMigrationDto>> implementationMock;
        IRequestHandler<FilteredRoleActivityUserMigrationQuery, List<UserMigrationDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<UserMigration, UserMigrationDto>>();
            handler = new FilteredRoleActivityUserMigrationQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            FilteredRoleActivityUserMigrationQuery request = new("name", 2);
            List<UserMigrationDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345", Name = "name", Email = "name@email.com" }
            };

            implementationMock.Setup(x => x.ToListModelBy(It.IsAny<Expression<Func<UserMigration, bool>>>())).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
