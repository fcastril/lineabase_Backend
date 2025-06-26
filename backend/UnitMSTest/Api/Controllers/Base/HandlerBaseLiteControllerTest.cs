using Moq;
using MediatR;
using Api.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Api.Controllers.Base {
  [TestClass]
  public class HandlerBaseLiteControllerTest<DTO, Controller>
    where DTO : class
    where Controller : HandlerBaseLiteController<DTO> {

    protected Mock<IMediator> _mockMediator = new();
    protected Controller _controller;
    protected DTO _dto;

    [TestInitialize]
    public void TestInitialize() {
      _mockMediator = new Mock<IMediator>();
      _dto = default(DTO);
    }

    [TestMethod]
    public void HandlerResponseTest() {
      HandlerBaseLiteController<DTO> controller = new HandlerBaseLiteController<DTO>(_mockMediator.Object);
      var result = controller.HandlerResponse(_dto);
      Assert.IsNotNull(result);
    }
  }
}
