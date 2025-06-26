using Domain.Port;
using Infrastructure;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Infrastructure {
  [TestClass]
  public class DependencyInjectionTests {
    private IConfiguration _configuration;
    private IServiceCollection _services;

    [TestInitialize]
    public void Setup() {
      var inMemorySettings = new Dictionary<string, string> {
        { "ConfigurateCosmosDB:ConnectionString", "mongodb://localhost:27017" },
        { "ConfigurateCosmosDB:DatabaseName", "TestDatabase" }
      };

      _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
      _services = new ServiceCollection();
    }

    [TestMethod]
    public void AddDependencyInjectionsInfrastructure_ShouldConfigureCosmosDB() {
      // Act
      _services.AddDependencyInjectionsInfrastructure(_configuration);
      var serviceProvider = _services.BuildServiceProvider();
      var config = serviceProvider.GetService<IConfigurateCosmosDB>();

      // Assert
      Assert.IsNotNull(config);
      Assert.AreEqual("mongodb://localhost:27017", config.ConnectionString);
      Assert.AreEqual("TestDatabase", config.DatabaseName);
    }

    [TestMethod]
    public void AddDependencyInjectionsInfrastructure_ShouldRegisterRepositories() {
      // Act
      _services.AddDependencyInjectionsInfrastructure(_configuration);
      var serviceProvider = _services.BuildServiceProvider();

      // Assert
      Assert.IsNotNull(serviceProvider.GetService<ISecurityRepository>());
      Assert.IsNotNull(serviceProvider.GetService<IRolRepository>());
      Assert.IsNotNull(serviceProvider.GetService<ICustomerRepository>());
    }

    [TestMethod]
    public void AddDependencyInjectionsInfrastructure_ShouldRegisterMainContextCosmos() {
      // Act
      _services.AddDependencyInjectionsInfrastructure(_configuration);
      var serviceProvider = _services.BuildServiceProvider();

      // Assert
      Assert.IsNotNull(serviceProvider.GetService<IMainContextCosmos>());
    }
  }
}
