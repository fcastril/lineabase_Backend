using Domain.Entities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Context.Auth.Specifications {
  [TestClass]
  public class UserSpecificationTests {
    [TestMethod]
    public void ExistByUsername_ShouldReturnCorrectExpression() {
      // Arrange
      var userName = "testuser";
      var id = "123";
      var roles = new List<Rol> { new Rol("1", "Admin", "Administrator role", true) };
      var user = new User ( userName : userName, email: "testuser@testuser.com", name: userName, password: "password123", roles );

      // Act
      var expression = UserSpecification.ExistByUsername(userName, id);
      var compiledExpression = expression.Compile();

      // Assert
      Assert.IsTrue(compiledExpression(user));
    }

    [TestMethod]
    public void ExistByUsername_ShouldEvaluateCorrectly() {
      // Arrange
      var userName = "testuser";
      var id = "123";
      var roles = new List<Rol> { new Rol("1", "Admin", "Administrator role", true) };
      var user = new User ( userName : userName, email: "testuser@testuser.com", name: userName, password: "password123", roles );

      var expression = UserSpecification.ExistByUsername(userName, id);
      var compiledExpression = expression.Compile();

      // Act
      var result = compiledExpression(user);

      // Assert
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void ExistByUsername_ShouldEvaluateIncorrectly() {
      // Arrange
      var userName = "testuser";
      var id = "123";
      var roles = new List<Rol> { new Rol("1", "Admin", "Administrator role", true) };
      var user = new User ( userName : "otheruser", email: "testuser@testuser.com", name: userName, password: "password123", roles );
      var expression = UserSpecification.ExistByUsername(userName, id);
      var compiledExpression = expression.Compile();

      // Act
      var result = compiledExpression(user);

      // Assert
      Assert.IsFalse(result);
    }
  }
}
