using Domain.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Common {
  [TestClass]
  public class TestableDomainModelExceptions : DomainModelExceptions {
    public TestableDomainModelExceptions(string code, string propName, string message) : base(code, propName, message) {
    }

    [TestMethod]
    public IEnumerable<object> GetEqualityComponentsPublic() {
      return GetEqualityComponents();
    }
  }

  [TestClass]
  public class DomainModelExceptionsTests {
    [TestMethod]
    public void Constructor_ShouldInitializeProperties() {
      // Arrange
      var code = "ERR001";
      var propName = "Name";
      var message = "Name is required";

      // Act
      var exception = new TestableDomainModelExceptions(code, propName, message);

      // Assert
      Assert.AreEqual(code, exception.Code);
      Assert.AreEqual(propName, exception.PropName);
      Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void GetEqualityComponents_ShouldReturnCorrectComponents() {
      // Arrange
      var code = "ERR001";
      var propName = "Name";
      var message = "Name is required";
      var exception = new TestableDomainModelExceptions(code, propName, message);
      System.String  componentString = "";

      // Act
      IEnumerable<System.Object> components = exception.GetEqualityComponentsPublic();

      foreach (var component in components) {
        if (component.Equals(code)) {
          componentString = component.ToString();
          break;
        }
      }

      // Assert
      Assert.AreEqual(code, componentString);
    }

    [TestMethod]
    public void Equals_ShouldReturnTrueForEqualObjects() {
      // Arrange
      var code = "ERR001";
      var propName = "Name";
      var message = "Name is required";
      var exception1 = new TestableDomainModelExceptions(code, propName, message);
      var exception2 = new TestableDomainModelExceptions(code, propName, message);

      // Act
      var areEqual = exception1.Equals(exception2);

      // Assert
      Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void Equals_ShouldReturnFalseForDifferentObjects() {
      // Arrange
      var exception1 = new TestableDomainModelExceptions("ERR001", "Name", "Name is required");
      var exception2 = new TestableDomainModelExceptions("ERR002", "Description", "Description is required");

      // Act
      var areEqual = exception1.Equals(exception2);

      // Assert
      Assert.IsFalse(areEqual);
    }

    [TestMethod]
    public void GetHashCode_ShouldReturnSameHashCodeForEqualObjects() {
      // Arrange
      var code = "ERR001";
      var propName = "Name";
      var message = "Name is required";
      var exception1 = new TestableDomainModelExceptions(code, propName, message);
      var exception2 = new TestableDomainModelExceptions(code, propName, message);

      // Act
      var hashCode1 = exception1.GetHashCode();
      var hashCode2 = exception2.GetHashCode();

      // Assert
      Assert.AreEqual(hashCode1, hashCode2);
    }

    [TestMethod]
    public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentObjects() {
      // Arrange
      var exception1 = new TestableDomainModelExceptions("ERR001", "Name", "Name is required");
      var exception2 = new TestableDomainModelExceptions("ERR002", "Description", "Description is required");

      // Act
      var hashCode1 = exception1.GetHashCode();
      var hashCode2 = exception2.GetHashCode();

      // Assert
      Assert.AreNotEqual(hashCode1, hashCode2);
    }
  }
}
