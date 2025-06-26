using Domain.Entities;
using Domain.AggregateModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Domain.Context.Auth.Metadata {
  [TestClass]
  public class RolMetadataTests {

    [TestMethod]
    public void NameMetadata_ShouldHaveCorrectValues() {
      // Arrange
      var expectedName = nameof(Rol.Name);
      var expectedDisplayName = nameof(Rol.Name);
      var expectedMaxLength = 100;
      var expectedMinLength = 5;

      // Act
      var metadata = RolMetadata.Name;

      // Assert
      Assert.AreEqual(expectedName, metadata.Tag);
      Assert.AreEqual(expectedName, metadata.LogicName);
      Assert.AreEqual(expectedMaxLength, metadata.Length);
      Assert.AreEqual(expectedMinLength, metadata.MinLength);
    }

    [TestMethod]
    public void DescriptionMetadata_ShouldHaveCorrectValues() {
      // Arrange
      var expectedName = nameof(Rol.Description);
      var expectedDisplayName = nameof(Rol.Description);
      var expectedMaxLength = 250;
      var expectedMinLength = 1;

      // Act
      var metadata = RolMetadata.Description;

      // Assert
      Assert.AreEqual(expectedName, metadata.Tag);
      Assert.AreEqual(expectedName, metadata.LogicName);
      Assert.AreEqual(expectedMaxLength, metadata.Length);
      Assert.AreEqual(expectedMinLength, metadata.MinLength);
    }
  }
}
