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
    public class GetByNameRepositoryMigrationTest
    {
        Mock<IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>> implementationMock;
        IRequestHandler<GetByNameRepositoryMigrationAsyncQuery, RepositoryMigrationDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto>>();
            handler = new GetByNameRepositoryMigrationAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            GetByNameRepositoryMigrationAsyncQuery request = new("RepositoryMigration");
            RepositoryMigrationDto dto = new()
            {
                Id = "1",
                Name = "RepositoryMigration"
            };

            implementationMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<RepositoryMigration, bool>>>())).ReturnsAsync(dto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(dto.Id, result.Id);
        }
    }
}
