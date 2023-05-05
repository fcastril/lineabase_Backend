using System;
namespace ServiceBus.HandlerAzureServiceBus
{
	public interface IServicesBusHandler
	{
        Task SendMessageQueue(object message, string queue);
    }
}

