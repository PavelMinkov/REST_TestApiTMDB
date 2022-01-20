using Newtonsoft.Json;

namespace TestApiTMDB.DataTransferObject
{
    class GuestSessionDto
    {
        [JsonProperty("guest_session_id")]
        public string guestSessionId { get; set; }
    }
}
