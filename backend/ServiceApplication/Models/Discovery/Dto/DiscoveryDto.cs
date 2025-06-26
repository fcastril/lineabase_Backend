using System;
using System.Collections.Generic;

namespace ServiceApplication.Dto
{
    public class DiscoveryDto : BaseDto
    {
        public CustomerDto Customer { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string Description { get; set; }
        public string StatusBenefit { get; set; }
        public DiscoveryDto()
        {


        }

    }
}