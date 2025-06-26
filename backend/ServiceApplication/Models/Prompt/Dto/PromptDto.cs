namespace ServiceApplication.Dto
{
    public class PromptDto : BaseDto
    {
        public PromptDto() { }

        public string Name { get; set; }
        public string Body { get; set; }
        public string Overrides { get; set; }
    }
}
