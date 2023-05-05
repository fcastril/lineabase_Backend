using System;
namespace ServicesBus.HandlerAzureServiceBus.Listener
{
	public interface IServicesListenerHandler
	{
        Func<EventQueue, Task> EventBusiness { get; set; }
        Task DeQueueAuto(string queue);
        bool DeQueue { get; set; }
    }
}

