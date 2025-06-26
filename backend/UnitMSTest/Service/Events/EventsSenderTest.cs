using Azure.Messaging.ServiceBus;
using BlobStorageMtow;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Events;
using ServiceApplication.Port;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UnitMSTest.Service
{
    [TestClass]
    public class EventsSenderTest
    {
        Mock<IConfiguration> configurationMock;
        Mock<ITableStorage<Transactions>> tableStorageMock;
        Mock<IMediator> mediatorMock;
        IMessageSender<CommGeneric> messageSender;

        [TestInitialize]
        public void Init()
        {
            configurationMock = new Mock<IConfiguration>();
            tableStorageMock = new Mock<ITableStorage<Transactions>>();
            mediatorMock = new Mock<IMediator>();
            messageSender = new EventsAsyncSender<CommGeneric>(configurationMock.Object, tableStorageMock.Object, mediatorMock.Object);
        }

        [TestMethod]
        public async Task SendCommSuccessfulTest()
        {
            CommGeneric comm = new();

            configurationMock.Setup(x => x.GetSection("ServicesBus:send").Value).Returns("Endpoint=sb://sb01azimtwindev.servicebus.windows.net/;SharedAccessKeyName=SendManageSahredAccessKey;SharedAccessKey=YgkSAM7wZCYgUBtXaKubHggYWEtaL4Z2V+ASbIbP3bs=");
            configurationMock.Setup(x => x.GetSection("ServicesBus:queueName").Value).Returns("iresponsequeue");
            configurationMock.Setup(x => x["ServicesBus:queueName"]).Returns("iresponsequeue");
            tableStorageMock.Setup(x => x.InsertData(It.IsAny<Transactions>()));

            await messageSender.SendCommAsync(comm);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task SendAsyncSuccessfulTest()
        {
            string discoveryId = "12345";
            CommGeneric comm = new();
            DiscoveryDto dto = new() { Id = "12345", Description = "Description" };

            configurationMock.Setup(x => x.GetSection("ServicesBus:send").Value).Returns("Endpoint=sb://sb01azimtwindev.servicebus.windows.net/;SharedAccessKeyName=SendManageSahredAccessKey;SharedAccessKey=YgkSAM7wZCYgUBtXaKubHggYWEtaL4Z2V+ASbIbP3bs=");
            configurationMock.Setup(x => x.GetSection("ServicesBus:queueName").Value).Returns("iresponsequeue");
            configurationMock.Setup(x => x["ServicesBus:queueName"]).Returns("iresponsequeue");
            tableStorageMock.Setup(x => x.InsertData(It.IsAny<Transactions>()));
            mediatorMock.Setup(x => x.Send(It.IsAny<GetByIdAsyncQuery<Discovery, DiscoveryDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(dto);
            mediatorMock.Setup(x => x.Send(It.IsAny<UpdateAsyncCommand<Discovery, DiscoveryDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(dto);

            await messageSender.SendAsync(comm);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task SendCommThrowExceptionTest()
        {
            CommGeneric comm = new();
            await messageSender.SendCommAsync(comm);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceBusException))]
        public async Task SendCommThrowExceptionServiceBusTest()
        {
            CommGeneric comm = new();
            configurationMock.Setup(x => x.GetSection("ServicesBus:send").Value).Returns("Endpoint=sb://sb01azimtwindev.servicebus.windows.net/;SharedAccessKeyName=SendManageSahredAccessKey;SharedAccessKey=YgkSAM7wZCYgUBtXaKubHggYWEtaL4Z2V+ASbIbP3bs=");
            configurationMock.Setup(x => x.GetSection("ServicesBus:queueName").Value).Returns("asd");
            await messageSender.SendCommAsync(comm);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task SendThrowExceptionTest()
        {
            CommGeneric comm = new();
            await messageSender.SendAsync(comm);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceBusException))]
        public async Task SendThrowExceptionServiceBusTest()
        {
            DiscoveryDto dto = new() { Id = "12345", Description = "Description" };
            CommGeneric comm = new();
            configurationMock.Setup(x => x.GetSection("ServicesBus:send").Value).Returns("Endpoint=sb://sb01azimtwindev.servicebus.windows.net/;SharedAccessKeyName=SendManageSahredAccessKey;SharedAccessKey=YgkSAM7wZCYgUBtXaKubHggYWEtaL4Z2V+ASbIbP3bs=");
            configurationMock.Setup(x => x.GetSection("ServicesBus:queueName").Value).Returns("asd");
            mediatorMock.Setup(x => x.Send(It.IsAny<GetByIdAsyncQuery<Discovery, DiscoveryDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(dto);
            mediatorMock.Setup(x => x.Send(It.IsAny<UpdateAsyncCommand<Discovery, DiscoveryDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(dto);
            await messageSender.SendAsync(comm);
        }
    }
}
