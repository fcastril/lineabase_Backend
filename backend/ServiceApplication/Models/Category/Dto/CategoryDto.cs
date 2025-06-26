using System;

namespace ServiceApplication.Dto
{
    public class CategoryDto : BaseDto
    {
		public string Description { get; set; }
		public bool Status { get; set; }

        public CategoryDto()
        {
            
            
        }
        
    }
}