using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Application
    {
        [Key]
        public int No { get; set; }
        public required string Purpose { get; set; }
        public required string Route { get; set; }
        public required string TravelMode { get; set; }
        public required DateTime ArrivalDate { get; set; }
        public required int Period { get; set; } 
        public int AmountOfMoney { get; set; }
        public string? MoneyType { get; set; }
        public string? Status { get; set; }

        public required string ApplicantNIC { get; set; }
        public required string ApplicantNationality { get; set; }

        [ForeignKey("ApplicantNIC,ApplicantNationality")]
        public virtual Applicant Applicant { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
