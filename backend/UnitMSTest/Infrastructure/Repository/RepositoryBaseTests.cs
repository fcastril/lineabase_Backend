using Moq;
using System;
using Domain.Port;
using Infrastructure;
using MongoDB.Driver;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Infrastructure.Repository {
  [TestClass]
  public class RepositoryBaseTests {
    private Mock<IMainContextCosmos> _mockContext;
    private Mock<IMongoCollection<TestEntity>> _mockCollection;
    private RepositoryBase<TestEntity> _repository;

    [TestInitialize]
    public void Setup() {
      _mockContext = new Mock<IMainContextCosmos>();
      _mockCollection = new Mock<IMongoCollection<TestEntity>>();
      _mockContext.Setup(c => c.GetCollection<TestEntity>(It.IsAny<string>())).Returns(_mockCollection.Object);
      _repository = new RepositoryBase<TestEntity>(_mockContext.Object);
    }

    [TestMethod]
    public async Task CreateModel_ShouldInsertEntity() {
      var entity = new TestEntity { Id = "1", Name = "Test" };
      await _repository.CreateModel(entity);
      _mockCollection.Verify(c => c.InsertOneAsync(entity, null, default), Times.Once);
    }

    [TestMethod]
    public async Task DeleteModel_ShouldDeleteEntity() {
      var property = "Id";
      var value = "1";
      await _repository.DeleteModel(property, value);
      _mockCollection.Verify(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<TestEntity>>(), default), Times.Once);
    }

    //[TestMethod]
    //public async Task UpdateModel_ShouldUpdateEntity() {
    //  var entity = new TestEntity { Id = "1", Name = "Updated" };
    //  await _repository.UpdateModel(entity);
    //  _mockCollection.Verify(c => c.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), entity, null, default), Times.Once);
    //}

    //[TestMethod]
    //public async Task ToListModel_ShouldReturnAllEntities() {
    //  var entities = new List<TestEntity> { new TestEntity { Id = "1", Name = "Test" } };
    //  _mockCollection.Setup(c => c.Find(It.IsAny<FilterDefinition<TestEntity>>(), null).ToListAsync(default)).ReturnsAsync(entities);
    //  var result = await _repository.TolistModel();
    //  Assert.AreEqual(entities, result);
    //}
  }

  public class TestEntity {
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime? DateLastUpdate { get; set; }
  }
}
