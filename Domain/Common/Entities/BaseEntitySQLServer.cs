using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
	public class BaseEntitySQLServer : BaseGeneral
	{
		public BaseEntitySQLServer(): base()
		{

		}
		public BaseEntitySQLServer(Guid id) : base()
		{
			Id = id;
		}
		public BaseEntitySQLServer(Guid id, DateTime dateCreation, string status, DateTime? dateLastUpdate) : base(dateCreation, status, dateLastUpdate)
		{
			Id = id;
		}

		[Key]
		public Guid Id { get; private set; } = Guid.NewGuid();
	}
}

