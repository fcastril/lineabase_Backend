using System;
using System.Net.NetworkInformation;

namespace Domain.Common
{
	public class BaseGeneral
	{
        public BaseGeneral(DateTime dateCreation, string status, DateTime? dateLastUpdate )
        {
            DateCreation = dateCreation;
            DateLastUpdate = dateLastUpdate;
            Status = status;

        }
        public BaseGeneral()
        {
            DateCreation = DateTime.UtcNow;
            DateLastUpdate = null;
            Status = States.Active.ToString();
        }
        public DateTime DateCreation { get; private set; }
        public string Status { get; private set; }
        public DateTime? DateLastUpdate { get; private set; }
    }
}

