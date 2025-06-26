namespace ServiceApplication.Events
{
    public class CommGeneric: MessageQueue
    {
        public string Organization { get; set; } = string.Empty;
        public string PersonalToken { get; set; } = string.Empty;
    }
}