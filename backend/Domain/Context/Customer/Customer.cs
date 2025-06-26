
using Domain.Common;
namespace Domain.Entities
{
	public class Customer : BaseEntity
	{
		public Customer() { }
		public Customer(
			string name,
			string email,
			int developers,
			Sector sector,
			bool marketingemails,
			bool newsupdate,
			bool productionprocess,
			bool status)
		{
			Name = name;
			Email = email;
			Developers = developers;
			Sector = sector;
			MarketingEmails = marketingemails;
			NewsUpdate = newsupdate;
			ProductionProcess = productionprocess;
			Status = status;
		}
		public string Name { get; private set; }
		public string Email { get; private set; }
		public int Developers { get; private set; }
		public Sector Sector { get; private set; }
		public bool MarketingEmails { get; private set; }
		public bool NewsUpdate { get; private set; }
		public bool ProductionProcess { get; private set; }
		public bool Status { get; private set; }
	}
}