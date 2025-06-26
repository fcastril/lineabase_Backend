namespace ServiceApplication.Dto
{
    public class ConnectToolDto : BaseDto
    {
        public ConnectToolDto() { }

        public string DiscoveryId { get; set; }
        public string Organization { get; set; }
        public string PAT {  get; set; }
    }
}
