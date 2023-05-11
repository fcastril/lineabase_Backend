using System;
using System.Collections.Generic;
using Domain.AggregateModels;
using Domain.Common;

namespace Domain.Entities
{
    public class Rol : BaseEntitySQLServer
    {
        public Rol()
        {

        }
        public Rol(Guid id, string name, string description, bool root, DateTime dateCreated, string status, DateTime? dateLastUpdate)
            : base(id, dateCreated, status, dateLastUpdate)
        {
            setProperties(name, description, root);
        }

        private void setProperties(string name, string description, bool root)
        {
            Name = ValueObjectString.Create(name, RolMetadata.Name).Value;
            Description = ValueObjectString.Create(description, RolMetadata.Description).Value;
            Root = root;
        }
        public ValueObjectString Name { get; private set; }
        public ValueObjectString Description { get; private set; }
        public bool Root { get; private set; }
        public virtual List<User> Users { get; set; }

    }
}