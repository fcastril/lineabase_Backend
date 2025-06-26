using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class FilterNameEmailUserMigrationTest
    {
        Mock<IBaseServiceApplication<UserMigration, UserMigrationDto>> implementationMock;
        IRequestHandler<FilteredNameEmailUserMigrationQuery, List<UserMigrationDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<UserMigration, UserMigrationDto>>();
            handler = new FilteredNameEmailUserMigrationQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            FilteredNameEmailUserMigrationQuery request = new("name");
            List<UserMigrationDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345", Name = "name", Email = "name@email.com" }
            };

            implementationMock.Setup(x => x.ToListModelBy(u => u.Name.ToLower().Contains(request.search.ToLower()) || u.Email.ToLower().Contains(request.search.ToLower()))).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest2()
        {
            FilteredNameEmailUserMigrationQuery request = new("");
            List<UserMigrationDto> listResponse = new()
            {
                new() { Id = "1", DiscoveryId = "12345", Name = "name" },
                new() { Id = "2", DiscoveryId = "12345", Name = "last" }
            };

            implementationMock.Setup(x => x.TolistModel()).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
