using Azure.Messaging.ServiceBus;
using BlobStorageMtow;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication.Events;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class EventsAsyncListenerTests
    {
        private Mock<IConfiguration> configurationMock;
        private Mock<ITableStorage<Transactions>> tableStorageMock;
        private Mock<HttpMessageHandler> httpMessageHandlerMock;
        private Mock<ServiceBusProcessor> processorMock;
        private HttpClient httpClient;
        private EventsAsyncListener listener;

        [TestInitialize]
        public void Init()
        {
            configurationMock = new Mock<IConfiguration>();
            tableStorageMock = new Mock<ITableStorage<Transactions>>();
            httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpClient = new HttpClient(httpMessageHandlerMock.Object);

            configurationMock
                .Setup(x => x.GetSection("ServicesBus:queueName").Value)
                .Returns("iresponsequeue");

            configurationMock
                .Setup(x => x.GetSection("ServicesBus:listen").Value)
                .Returns("Endpoint=sb://sb01azimtwindev.servicebus.windows.net/;SharedAccessKeyName=ListenerManageSahredAccessKey;SharedAccessKey=wFxCeV7m7vhJzyiemdqLeYUNXnmsdlrHh+ASbFYVhlo=");

            processorMock = new Mock<ServiceBusProcessor>();
            processorMock
                .Setup(x => x.StartProcessingAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            processorMock
                .Setup(x => x.StopProcessingAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            processorMock
                .Setup(x => x.CloseAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            listener = new EventsAsyncListener(configurationMock.Object, tableStorageMock.Object, processorMock.Object);
        }

        [TestMethod]
        public async Task StartAsync_ShouldStartProcessor()
        {
            await listener.StartAsync(CancellationToken.None);
            processorMock.Verify(x => x.StartProcessingAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task StopAsync_ShouldStopAndCloseProcessor()
        {
            processorMock
                .Setup(x => x.StopProcessingAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            processorMock
                .Setup(x => x.CloseAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await listener.StopAsync(CancellationToken.None);

            processorMock.Verify(
                x => x.StopProcessingAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );

            processorMock.Verify(
                x => x.CloseAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ProcessErrorAsync_ShouldLogError()
        {
            var exception = new Exception("Test exception");
            var args = new ProcessErrorEventArgs(
                exception,
                ServiceBusErrorSource.ProcessMessageCallback,
                "test-entity",
                "test-queue",
                CancellationToken.None
            );

            await listener.ProcessErrorAsync(args);

            tableStorageMock.Verify(
                x => x.InsertData(It.Is<Transactions>(t =>
                    t.OriginCategory == "Error" &&
                    t.Input.Contains("Test exception") // Verifica que el error se registró
                )),
                Times.Once
            );
        }
    }
}
