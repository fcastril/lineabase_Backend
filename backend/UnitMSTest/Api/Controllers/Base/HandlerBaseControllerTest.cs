using Moq;
using System;
using MediatR;
using Api.Base;
using Util.Common;
using Domain.Common;
using FluentValidation;
using System.Threading;
using ServiceApplication.CQRS;
using FluentValidation.Results;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Api.Controllers.Base {
  [TestClass]
  public class HandlerBaseControllerTest<ENT, DTO, Controller> : HandlerBaseLiteControllerTest<DTO, Controller>
          where ENT : class, new()
          where DTO : class, new()
          where Controller : HandlerBaseController<ENT, DTO> {

    protected Controller _controller;
    protected Mock<IValidator<DTO>> _mockValidator;
    protected BaseEntity _entity;

    [TestInitialize]
    public void TestInitialize() {
      base.TestInitialize();
      _mockValidator = new Mock<IValidator<DTO>>();
      SetMockMediator();
      SetMockValidator();
      _controller = (Controller)Activator.CreateInstance(typeof(Controller), _mockMediator.Object, _mockValidator.Object);
    }

    protected void SetMockMediator() {
      _mockMediator = new Mock<IMediator>();
      _mockMediator.Setup(x => x.Send(It.IsAny<CreateAsyncCommand<ENT, DTO>>(), default)).ReturnsAsync(_dto);
    }

    protected void SetMockValidator() {
      _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<DTO>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
    }

    protected void SetMockValidatorWithErrors() {
      // Crear una lista de errores
      var errors = new List<ValidationFailure> {
        new ValidationFailure("property", "error")
      };

      // Crear un ValidationResult con los errores
      var validationResult = new ValidationResult(errors);

      // Configurar el mock para que retorne el ValidationResult con errores
      _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<DTO>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);
    }

    protected void SetMockBaseEntity() {
      BaseEntity _baseEntity = new BaseEntity(){
        DateCreation = It.IsAny<DateTime>(),
        DateLastUpdate = It.IsAny<DateTime>()
      };
      _entity = _baseEntity;
    }

    [TestMethod]
    public void ConstructorTest() {
      Assert.IsNotNull(_controller);
    }

    [TestMethod]
    public void CreateWithoutErrorsTest() {
      DTO dto = new DTO();
      var result = _controller.Create(dto);
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void CreateWithErrorsTest() {
      var dto = new DTO();
      SetMockValidatorWithErrors();

      Assert.ThrowsExceptionAsync<Util.Ex.DomainException>(() => _controller.Create(dto));
    }

    [TestMethod]
    public void UpdateWithoutErrorsTest() {
      DTO dto = new DTO();
      var result = _controller.Update(dto);
      Assert.IsNotNull(result);
    }
    [TestMethod]
    public void UpdateWithErrorsTest() {
      DTO dto = new DTO();
      SetMockValidatorWithErrors();

      Assert.ThrowsExceptionAsync<Util.Ex.DomainException>(() => _controller.Update(dto));
    }

    [TestMethod]
    public void GetByIdWithoutErrorsTest() {
      var result = _controller.GetById(It.IsAny<string>());
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void GetWithoutErrorsTest() {
      var result = _controller.Get();
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void DeleteWithoutErrorsTest() {
      var dto = It.IsAny<string>();
      var result = _controller.Delete(dto);
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void PaginatorWithoutErrorsTest() {
      var dto = new Paginate<DTO>();
      var result = _controller.Paginator(dto);
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void GetByWithoutErrorsTest() {
      var property = It.IsAny<string>();
      var value = It.IsAny<string>();
      var result = _controller.GetBy(property, value);
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void GetListByWithoutErrorsTest() {
      var property = It.IsAny<string>();
      var value = It.IsAny<string>();
      var result = _controller.GetListBy(property, value);
      Assert.IsNotNull(result);
    }
  }
}
