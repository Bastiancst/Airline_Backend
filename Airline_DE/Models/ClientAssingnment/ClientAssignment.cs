using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline_DE.Models.ClientAssingnment
{
    public class ClientAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid AirplanePlanningId { get; set; }
        public Guid ClientId { get; set; }

    }
}
