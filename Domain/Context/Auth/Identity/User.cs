using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : BaseEntitySQLServer
    {
        public User()
        {

        }
        public User(
            Guid id,
            string userName,
            string email,
            string name,
            string password,
            DateTime dateCreated,
            string status,
            DateTime? dateLastUpdated,
            Rol rol) :
            base(id, dateCreated, status, dateLastUpdated)
        {
            UserName = ValueObjectString.Create(userName, UserMetadata.Username).Value;
            Email = email;
            Name = ValueObjectString.Create(name, UserMetadata.Name).Value;
            Password = ValueObjectString.Create(password, UserMetadata.Password).Value;
            Rol = rol;
        }


        public ValueObjectString UserName { get; private set; }
        public string Email { get; private set; }
        public ValueObjectString Name { get; private set; }
        public ValueObjectString Password { get; private set; }
        public Rol Rol { get; private set; }
    }
}
 