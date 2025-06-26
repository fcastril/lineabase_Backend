using System;

namespace ServiceApplication.Dto
{
    public class ToolDto : BaseDto
    {
		public string Description { get; set; }
		public bool Status { get; set; }

        public ToolDto()
        {
            
            
        }
        
    }
}