using System.ComponentModel.DataAnnotations;

namespace Airline_DE.Models.ChatMessage
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; } 
        public Guid ClientId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Message { get; set; }
        public DateTime TimesTamp { get; set; }
    }
}
