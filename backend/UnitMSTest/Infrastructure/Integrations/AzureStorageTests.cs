using Moq;
using Azure;
using System.IO;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Infrastructure.Integrations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Infrastructure.Integrations {
  [TestClass]
  public class AzureStorageTests {
    private Mock<IConfiguration> _mockConfiguration;
    private AzureStorage _azureStorage;
    private Mock<BlobContainerClient> _mockContainerClient;
    private Mock<BlobClient> _mockBlobClient;
    private Mock<BlobServiceClient> _mockBlobServiceClient;

    [TestInitialize]
    public void Setup() {
      _mockConfiguration = new Mock<IConfiguration>();
      _mockConfiguration.Setup(c => c["Storage:conectionString"]).Returns("UseDevelopmentStorage=true");

      _mockContainerClient = new Mock<BlobContainerClient>();
      _mockBlobClient = new Mock<BlobClient>();
      _mockBlobServiceClient = new Mock<BlobServiceClient>("UseDevelopmentStorage=true");

      _mockContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(_mockBlobClient.Object);
      _mockBlobServiceClient.Setup(b => b.GetBlobContainerClient(It.IsAny<string>())).Returns(_mockContainerClient.Object);

      _azureStorage = new AzureStorage(_mockConfiguration.Object, _mockContainerClient.Object, _mockBlobServiceClient.Object);
    }

    [TestMethod]
    public async Task UploadFileStorage_ShouldReturnBlobUri() {
      // Arrange
      var file = new byte[] { 1, 2, 3 };
      var name = "testfile";
      var ext = ".txt";
      var container = "testcontainer";

      _mockBlobClient.Setup(b => b.UploadAsync(It.IsAny<Stream>(), true, default)).ReturnsAsync(Mock.Of<Response<BlobContentInfo>>());
      _mockBlobClient.Setup(b => b.Uri).Returns(new System.Uri("https://example.com/testfile.txt"));

      // Act
      var result = await _azureStorage.UploadFileStorage(file, name, ext, container);

      // Assert
      Assert.AreEqual("https://example.com/testfile.txt", result);
    }

    [TestMethod]
    public async Task UploadFileStorage_ShouldReturnEmptyString_WhenFileIsNull() {
      // Act
      var result = await _azureStorage.UploadFileStorage(null, "testfile", ".txt", "testcontainer");

      // Assert
      Assert.AreEqual(string.Empty, result);
    }

    //[TestMethod]
    //public async Task DownloadFileStorage_ShouldReturnStream() {
    //  // Arrange
    //  var fileName = "testfile.txt";
    //  var container = "testcontainer";
    //  var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 });

    //  _mockBlobClient.Setup(b => b.OpenRead(It.IsAny<BlobOpenReadOptions>(), default)).Returns(memoryStream);
    //  _mockContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(_mockBlobClient.Object);

    //  // Act
    //  var result = await _azureStorage.DownloadFileStorage(fileName, container);

    //  // Assert
    //  Assert.IsNotNull(result);
    //  Assert.AreEqual(memoryStream, result);
    //}

    //[TestMethod]
    //public async Task ConectionStorage_ShouldInitializeContainerClient_WhenContainerClientIsNull() {

    //  // Arrange
    //  var container = "testcontainer";

    //  // Act
    //  _azureStorage.GetType().GetMethod("ConectionStorage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(_azureStorage, new object[] { container });

    //  // Assert
    //  _mockBlobServiceClient.Verify(b => b.GetBlobContainerClient(container), Times.Once);
    //}

    [TestMethod]
    public async Task ConectionStorage_ShouldNotInitializeContainerClient_WhenContainerClientIsNotNull() {
      // Arrange
      var container = "testcontainer";
      _azureStorage.GetType().GetField("containerClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
          .SetValue(_azureStorage, _mockContainerClient.Object);

      // Act
      _azureStorage.GetType().GetMethod("ConectionStorage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(_azureStorage, new object[] { container });

      // Assert
      _mockBlobServiceClient.Verify(b => b.GetBlobContainerClient(It.IsAny<string>()), Times.Never);
    }
  }
}
