using System.Text.Json.Serialization;

namespace Airline_DE.Models.Payment.DTOs
{
    public class CreateTransactionRequestDTO
    {
        [JsonPropertyName("buy_order")]
        public string BuyOrder { get; set; }
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("return_url")]
        public string? ReturnUrl { get; set; }
    }
}
