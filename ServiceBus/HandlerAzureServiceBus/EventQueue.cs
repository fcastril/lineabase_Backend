using System;
namespace ServicesBus.HandlerAzureServiceBus
{
	public class EventQueue
	{
		public EventQueue()
		{

		}
		public string CodeElevator { get; set; }
		public string CodeMovement { get; set; }
        public int Priority { get; set; }
		public int Floor  { get; set; }
	}
}

