using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLServer
{
	public partial class MainContextSQLServer
	{
		public virtual DbSet<BaseTable> BaseTable { get; set; }
	}
}

