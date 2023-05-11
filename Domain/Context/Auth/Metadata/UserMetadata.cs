using System;
using Domain.Common;
using Domain.Entities;

namespace Domain.Entities
{
	public class UserMetadata
	{
        public static Metadata Username => new(nameof(User.UserName), nameof(User.UserName), 20, 5);
        public static Metadata Password => new(nameof(User.Password), nameof(User.Password), 20, 5);
        public static Metadata Name => new(nameof(User.Name), nameof(User.Name), 250);

    }
}

