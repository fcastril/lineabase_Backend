using Domain.Common;

namespace Domain.Entities
{
    public class Prompt : BaseEntity
    {
        public Prompt() { }

        public Prompt(string name, string body, string overrides)
        {
            Name = name;
            Body = body;
            Overrides = overrides;
        }

        public string Name { get; private set; }
        public string Body { get; private set; }
        public string Overrides { get; private set; }
    }
}