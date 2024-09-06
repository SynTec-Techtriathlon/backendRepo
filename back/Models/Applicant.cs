using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Applicant
    {
        public required string NIC { get; set; }
        public required string Nationality { get; set; }
        public required string FullName { get; set; }
        public required string Gender { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string BirthPlace { get; set; }
        public required int Height { get; set; }
        public required string Address { get; set; }
        public required string TelNo { get; set; }
        public string? Email { get; set; }
        public string? Occupation { get; set; }
        public string? OccupationAddress { get; set; }
        public required string? MaritalStatus { get; set; }

        public virtual Passport Passport { get; set; }
        public virtual Spouse Spouse { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
