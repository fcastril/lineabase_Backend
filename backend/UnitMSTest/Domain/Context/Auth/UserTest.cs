using Domain.Entities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Context.Auth {
  [TestClass]
  public class UserTests {
    [TestMethod]
    public void Constructor_ShouldInitializeProperties() {
      // Arrange
      var userName = "testuser";
      var email = "testuser@example.com";
      var name = "Test User";
      var password = "password123";

      //TO-DO => REVISAR PORQUE NO PERMITE CREAR UNA LISTA DE ROLES CON MAS DE UN REGISTRO
      //var roles = new List<Rol> { new Rol("1", "Admin", "Administrator role", true), new Rol("2", "User", "User role", false) };

      var roles = new List<Rol> { new Rol("1", "Admin", "Administrator role", true) };

      // Act
      var user = new User(userName, email, name, password, roles);

      // Assert
      Assert.AreEqual(userName, user.UserName);
      Assert.AreEqual(email, user.Email);
      Assert.AreEqual(name, user.Nombre);
      Assert.AreEqual(password, user.Password);
      Assert.AreEqual(roles, user.Roles);
    }

    [TestMethod]
    public void DefaultConstructor_ShouldInitializePropertiesToDefaultValues() {
      // Act
      var user = new User();

      // Assert
      Assert.IsNull(user.UserName);
      Assert.IsNull(user.Email);
      Assert.IsNull(user.Nombre);
      Assert.IsNull(user.Password);
      Assert.IsNull(user.Roles);
    }

    [TestMethod]
    public void UserNameProperty_ShouldGetAndSetValues() {
      // Arrange
      var user = new User();
      var userName = "newuser";

      // Act
      user.GetType().GetProperty("UserName").SetValue(user, userName);

      // Assert
      Assert.AreEqual(userName, user.UserName);
    }

    [TestMethod]
    public void EmailProperty_ShouldGetAndSetValues() {
      // Arrange
      var user = new User();
      var email = "newuser@example.com";

      // Act
      user.GetType().GetProperty("Email").SetValue(user, email);

      // Assert
      Assert.AreEqual(email, user.Email);
    }

    [TestMethod]
    public void NombreProperty_ShouldGetAndSetValues() {
      // Arrange
      var user = new User();
      var name = "New User";

      // Act
      user.GetType().GetProperty("Nombre").SetValue(user, name);

      // Assert
      Assert.AreEqual(name, user.Nombre);
    }

    [TestMethod]
    public void PasswordProperty_ShouldGetAndSetValues() {
      // Arrange
      var user = new User();
      var password = "newpassword";

      // Act
      user.GetType().GetProperty("Password").SetValue(user, password);

      // Assert
      Assert.AreEqual(password, user.Password);
    }

    [TestMethod]
    public void RolesProperty_ShouldGetAndSetValues() {
      // Arrange
      var user = new User();
      Rol _rol = new Rol("Admin", "Administrator role", true);
      var roles = new List<Rol> { _rol };

      // Act
      user.GetType().GetProperty("Roles").SetValue(user, roles);

      // Assert
      Assert.AreEqual(roles, user.Roles);
    }
  }
}


//}
