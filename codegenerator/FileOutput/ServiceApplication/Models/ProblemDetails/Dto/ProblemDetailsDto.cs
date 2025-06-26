using System;

namespace ServiceApplication.Dto
{
    public class ProblemDetailsDto : BaseDto
    {
		public Discovery Discovery { get; set; }
		public Category Category { get; set; }
		public Problem Problem { get; set; }
		public int Impact { get; set; }
		public Frecuency Frecuency { get; set; }
		public string Description { get; set; }

        public ProblemDetailsDto()
        {
            
            
        }
        
    }
}