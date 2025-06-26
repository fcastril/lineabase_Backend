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
    public class ToListRepositoryAnalizeGenAIRepositoryIdTest
    {
        Mock<IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>> implementationMock;
        IRequestHandler<ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery, List<RepositoryAnalizeGenAIDto>> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<RepositoryAnalizeGenAI, RepositoryAnalizeGenAIDto>>();
            handler = new ToListRepositoryAnalizeGenAIRepositoryIdAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery request = new("12345");
            List<RepositoryAnalizeGenAIDto> listResponse = new()
            {
                new() { Id = "1", RepositoryId = "12345" }
            };

            implementationMock.Setup(x => x.TolistDtoBy(It.IsAny<Expression<Func<RepositoryAnalizeGenAI, bool>>>())).ReturnsAsync(listResponse);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1", result[0].Id);
        }
    }
}
