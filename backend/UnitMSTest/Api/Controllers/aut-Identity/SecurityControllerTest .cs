using Api.Controllers;
using Domain.Entities;
using ServiceApplication.Dto;
using System.Collections.Generic;
using UnitMSTest.Api.Controllers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Api.Controllers {
  [TestClass]
  public class SecurityControllerTest : HandlerBaseControllerTest<User, UserDto, SecurityController> {
    [TestMethod]
    public void InicialSesionTest() {
      var _login = new Login() {
        Expira = new System.DateTime().Date,
        Password = "",
        Profile = new List<Rol>(),
        Token = "",
        UserName = ""
      };

      var _securityController = new SecurityController(_mockMediator.Object, _mockValidator.Object).InicialSesion(_login);

      Assert.IsNotNull(_securityController);
    }
  }
}
