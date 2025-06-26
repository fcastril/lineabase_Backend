using System;

namespace ServiceApplication.Dto
{
	public class CustomerDto : BaseDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public int Developers { get; set; }
		public SectorDto Sector { get; set; }
		public bool MarketingEmails { get; set; }
		public bool NewsUpdate { get; set; }
		public bool ProductionProcess { get; set; }
		public bool Status { get; set; }
		public CustomerDto()
		{


		}

	}
}