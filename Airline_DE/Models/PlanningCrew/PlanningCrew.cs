using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Airline_DE.Models.PlanningCrew
{
    public class PlanningCrew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AirplanePlanningGuid { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
