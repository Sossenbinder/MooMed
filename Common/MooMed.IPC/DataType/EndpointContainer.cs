using Newtonsoft.Json;

namespace MooMed.IPC.DataType
{
    public class Endpoints
    {
        [JsonProperty("")]
        public string Address { get; set; }


        [JsonProperty("AccountService")]
        public string AccountService
        {
            set => Address = value;
        }
    }

    public class EndpointContainer
    {
        public Endpoints Endpoints { get; set; }
    }
}
