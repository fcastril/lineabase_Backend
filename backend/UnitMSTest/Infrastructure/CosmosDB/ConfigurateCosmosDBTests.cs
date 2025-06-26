using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Infrastructure.CosmosDB {  
  [TestClass]
  public class ConfigurateCosmosDBTests {
    [TestMethod]
    public void ConnectionString_ShouldGetAndSetValues() {
      // Arrange
      var config = new ConfigurateCosmosDB();
      var connectionString = "AccountEndpoint=https://example.documents.azure.com:443/;AccountKey=exampleKey;";

      // Act
      config.ConnectionString = connectionString;

      // Assert
      Assert.AreEqual(connectionString, config.ConnectionString);
    }

    [TestMethod]
    public void DatabaseName_ShouldGetAndSetValues() {
      // Arrange
      var config = new ConfigurateCosmosDB();
      var databaseName = "TestDatabase";

      // Act
      config.DatabaseName = databaseName;

      // Assert
      Assert.AreEqual(databaseName, config.DatabaseName);
    }

    [TestMethod]
    public void ConfigurateCosmosDB_ShouldImplementIConfigurateCosmosDB() {
      // Arrange
      var config = new ConfigurateCosmosDB();

      // Act & Assert
      Assert.IsInstanceOfType(config, typeof(IConfigurateCosmosDB));
    }
  }
}
