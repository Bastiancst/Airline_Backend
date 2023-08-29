using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Airline_DE.Models.User
{
    public class RecoverPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RecoveryToken { get; set; }
        public string RecoveryCode { get; set; }
        public DateTime RecoveryCodeTimeStamp { get; set; }
        public Guid UserId { get; set; }
    }
}
