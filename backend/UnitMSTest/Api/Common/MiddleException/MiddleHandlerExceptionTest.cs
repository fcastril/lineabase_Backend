using Moq;
using System;
using Util.Ex;
using Util.Common;
using BlobStorageMtow;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Api.Common.MiddleException;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Api.Common.MiddleException {
  [TestClass]
  public class MiddleHandlerExceptionTest {
    Mock<RequestDelegate> mockRequestDelegate;
    Mock<ITableStorage<Transactions>> mockTableStorage;
    Mock<IUtil> mockUtil;
    MiddleHandlerException middleHandlerException;

    [TestInitialize]
    public void Init() {
      mockTableStorage = new Mock<ITableStorage<Transactions>>();
      mockUtil = new Mock<IUtil>();
      mockRequestDelegate = new Mock<RequestDelegate>();
      middleHandlerException = new MiddleHandlerException(mockRequestDelegate.Object, mockTableStorage.Object, mockUtil.Object);
    }


    [TestMethod]
    public async Task InvokeAsyncTest() {
      mockRequestDelegate.Setup(x => x(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);
      await middleHandlerException.InvokeAsync(new DefaultHttpContext());

      Assert.IsNotNull(middleHandlerException);
    }

    [TestMethod]
    public async Task InvokeAsyncTestWithDomainExceptionOk() {
      mockRequestDelegate.Setup(x => x(It.IsAny<HttpContext>())).ThrowsAsync(new DomainExceptionOk("Error"));
      Assert.ThrowsExceptionAsync<Exception>(() => middleHandlerException.InvokeAsync(new DefaultHttpContext()));
    }

    [TestMethod]
    public async Task InvokeAsyncTestWithDomainException() {
      mockRequestDelegate.Setup(x => x(It.IsAny<HttpContext>())).ThrowsAsync(new DomainException("Error"));
      Assert.ThrowsExceptionAsync<Exception>(() => middleHandlerException.InvokeAsync(new DefaultHttpContext()));
    }

    [TestMethod]
    public async Task InvokeAsyncTestWithDomainExceptionAndNullHttpContext() {
      mockRequestDelegate.Setup(x => x(It.IsAny<HttpContext>())).ThrowsAsync(new DomainException("Error"));
      Assert.ThrowsExceptionAsync<Exception>(() => middleHandlerException.InvokeAsync(null));
    }

    [TestMethod]
    public async Task InvokeAsyncTestWithUnauthorizedAccessException() {
      mockRequestDelegate.Setup(x => x(It.IsAny<HttpContext>())).ThrowsAsync(new UnauthorizedAccessException("Error"));
      Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => middleHandlerException.InvokeAsync(new DefaultHttpContext()));
    }
  }
}
