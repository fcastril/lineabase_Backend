using System;
using Domain.Common;

namespace Domain.Entities
{
	public class BaseTable : BaseEntitySQLServer
	{
		public BaseTable(Guid id, string code, string description, DateTime dateCreation, string status, DateTime dateLastUpdate) : base(id, dateCreation, status, dateLastUpdate)
		{
			Code = ValueObjectString.Create(code, BaseTableMetadata.Code).Value;
			Description = ValueObjectString.Create(description, BaseTableMetadata.Description).Value;
		}
		public ValueObjectString Code { get; private set; }
		public ValueObjectString Description { get; set; }
    }
}

