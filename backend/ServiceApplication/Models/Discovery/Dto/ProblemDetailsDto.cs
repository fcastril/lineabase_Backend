using System;

namespace ServiceApplication.Dto
{
    public class ProblemDetailsDto : BaseDto
    {
		public DiscoveryDto Discovery { get; set; }
        public string DiscoveryId { get; set; }
        public CategoryDto Category { get; set; }
		public ProblemDto Problem { get; set; }
		public int Impact { get; set; }
		public FrecuencyDto Frecuency { get; set; }
		public string Description { get; set; }

        public ProblemDetailsDto()
        {
            
            
        }
        
    }
}