using System.IO;
using Domain.Port;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Integrations {
  public class AzureStorage : IAzureStorage {
    private IConfiguration Configuration { get; }
    private BlobContainerClient containerClient;
    private BlobServiceClient blobServiceClient;

    public AzureStorage(IConfiguration configuration, BlobContainerClient containerClient = null, BlobServiceClient blobServiceClient = null) {
      Configuration = configuration;
      this.containerClient = containerClient;
      this.blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadFileStorage(byte[] file, string name, string ext, string container) {
      if (file is null) {
        return string.Empty;
      }
      await ConectionStorage(container);

      string fileName = name + ext;

      BlobClient blobClient = containerClient.GetBlobClient(fileName);

      await blobClient.DeleteIfExistsAsync();

      using (var stream = new MemoryStream(file, writable: false)) {
        await blobClient.UploadAsync(stream, true);
      }

      return blobClient.Uri.ToString();
    }

    public async Task<Stream> DownloadFileStorage(string fileName, string container) {
      await ConectionStorage(container);
      var blob = containerClient.GetBlobClient(fileName);

      Stream blobStream = blob.OpenRead();
      return blobStream;
    }

    private async Task ConectionStorage(string container) {
      if (containerClient == null) {
        string connectionString = Configuration["Storage:conectionString"];
        blobServiceClient = new BlobServiceClient(connectionString);
        containerClient = blobServiceClient.GetBlobContainerClient(container);
        await containerClient.CreateIfNotExistsAsync();
      }
    }
  }


  //public class AzureStorage : IAzureStorage {
  //  private IConfiguration Configuration { get; }
  //  private BlobContainerClient containerClient;

  //  public AzureStorage(IConfiguration configuration, BlobContainerClient containerClient = null) {
  //    Configuration = configuration;
  //    this.containerClient = containerClient;
  //  }

  //  public async Task<string> UploadFileStorage(byte[] file, string name, string ext, string container) {
  //    if (file is null) {
  //      return string.Empty;
  //    }
  //    await ConectionStorage(container);

  //    string fileName = name + ext;

  //    BlobClient blobClient = containerClient.GetBlobClient(fileName);

  //    await blobClient.DeleteIfExistsAsync();

  //    using (var stream = new MemoryStream(file, writable: false)) {
  //      await blobClient.UploadAsync(stream, true);
  //    }

  //    return blobClient.Uri.ToString();
  //  }

  //  public async Task<Stream> DownloadFileStorage(string fileName, string container) {
  //    await ConectionStorage(container);
  //    var blob = containerClient.GetBlobClient(fileName);

  //    Stream blobStream = blob.OpenRead();
  //    return blobStream;
  //  }

  //  private async Task ConectionStorage(string container) {
  //    if (containerClient == null) {
  //      string connectionString = Configuration["Storage:conectionString"];
  //      BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
  //      containerClient = blobServiceClient.GetBlobContainerClient(container);
  //      await containerClient.CreateIfNotExistsAsync();
  //    }
  //  }
  
  
  //}
}
