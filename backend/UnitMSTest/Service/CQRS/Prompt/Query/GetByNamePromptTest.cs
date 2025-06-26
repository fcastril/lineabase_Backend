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
    public class GetByNamePromptTest
    {
        Mock<IBaseServiceApplication<Prompt, PromptDto>> implementationMock;
        IRequestHandler<GetByNamePromptAsyncQuery, PromptDto> handler;

        [TestInitialize]
        public void Init()
        {
            implementationMock = new Mock<IBaseServiceApplication<Prompt, PromptDto>>();
            handler = new GetByNamePromptAsyncQueryHandler(implementationMock.Object);
        }

        [TestMethod]
        public async Task HandleSuccessfulTest()
        {
            GetByNamePromptAsyncQuery request = new("Prompt");
            PromptDto dto = new()
            {
                Id = "1",
                Name = "Prompt",
                Body = "Test"
            };

            implementationMock.Setup(x => x.FirstOrDefautlModelBy(It.IsAny<Expression<Func<Prompt, bool>>>())).ReturnsAsync(dto);

            var result = await handler.Handle(request, new());

            Assert.IsNotNull(result);
            Assert.AreEqual(dto.Id, result.Id);
        }
    }
}
