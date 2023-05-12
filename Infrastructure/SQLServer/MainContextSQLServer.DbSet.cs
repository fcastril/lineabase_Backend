using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLServer
{
	public partial class MainContextSQLServer
	{
		public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
    }
}

