using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace ServiceBus.HandlerAzureServiceBus
{
	public class ServiceSenderHandler: IServicesBusHandler
    {
        ServiceBusClient client;

        ServiceBusSender sender;

        const int numOfMessages = 3;

        private readonly IConfiguration _configuration;

        public ServiceSenderHandler(IConfiguration configuration)
		{
            _configuration = configuration;
            InitialSettings();
        }

        private void InitialSettings()
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(_configuration["ServicesBus:send"], clientOptions);
        }

        public async Task SendMessageQueue(object message,string queue)
        {
            sender = client.CreateSender(queue);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            messageBatch.TryAddMessage(new ServiceBusMessage(JsonSerializer.Serialize(message)));
            try
            {
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
	}
}

