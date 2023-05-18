using System;

namespace ServiceApplication.Dto
{
    public class BaseGeneralDto
    {
        public DateTime DateCreation { get; set; }
        public string Status { get; set; }
        public DateTime? DateLastUpdate { get; set; }
    }
}