using System;

namespace ServiceApplication.Dto
{
    public class ProblemDto : BaseDto
    {
		public string Description { get; set; }
		public bool Status { get; set; }

        public ProblemDto()
        {
            
            
        }
        
    }
}