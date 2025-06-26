using Domain.Entities;
using Domain.AggregateModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Context.Auth.Metadata {
  [TestClass]
  public class RolSpecificationTests {
    [TestMethod]
    public void ExistRolByName_ShouldReturnCorrectExpression() {
      // Arrange
      Rol _rol = new Rol("Admin", "Administrator role", true);

      // Act
      var expression = RolSpecification.ExistRolByName(_rol.Name);
      var compiledExpression = expression.Compile();

      // Assert
      Assert.IsTrue(compiledExpression(_rol));
    }

    [TestMethod]
    public void ExistRolByDescription_ShouldReturnCorrectExpression() {
      // Arrange
      Rol _rol = new Rol("Admin", "Administrator role", true);

      // Act
      var expression = RolSpecification.ExistRolByDescription(_rol.Description);
      var compiledExpression = expression.Compile();

      // Assert
      Assert.IsTrue(compiledExpression(_rol));
    }
  }
}
