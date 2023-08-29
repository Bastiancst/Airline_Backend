using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Airline_DE.Models.PassengerAsignment
{
    public class PassengerAsignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PassengerId { get; set; }
        public Guid ClinetId { get; set; }

    }
}
