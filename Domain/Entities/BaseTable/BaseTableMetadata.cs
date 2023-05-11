using System;
using Domain.Common;

namespace Domain.Entities
{
	public class BaseTableMetadata 
	{
		public static Metadata Code => new(nameof(BaseTable.Code), nameof(BaseTable.Code), 20);
        public static Metadata Description => new(nameof(BaseTable.Description), nameof(BaseTable.Description), 250);
    }
}

