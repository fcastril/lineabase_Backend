using System;

namespace ServiceApplication.Dto
{
    public class CurrentToolDto : BaseDto
    {
		public Discovery Discovery { get; set; }
		public Tool Tool { get; set; }
		public Area Area { get; set; }
		public string NameServer { get; set; }

        public CurrentToolDto()
        {
            
            
        }
        
    }
}