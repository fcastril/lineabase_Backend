namespace CoreGenerator.Objects
{
    public class Entity
    {
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}