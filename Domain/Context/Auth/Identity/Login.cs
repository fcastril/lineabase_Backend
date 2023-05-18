using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Rol Profile { get; set; }
        public string Token { get; set; }
        public DateTime? Expira { get; set; }
    }
}
