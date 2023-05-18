using System.Collections.Generic;

namespace ServiceApplication.Dto
{
    public class RolDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Root { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
