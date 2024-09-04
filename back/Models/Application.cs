using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Application
    {
        [Key]
        public int No { get; set; }
        public string Purpose { get; set; }
        public string Route { get; set; }
        public string TravelMode { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int Period { get; set; } 
        public int AmountOfMoney { get; set; }
        public string MoneyType { get; set; }
        public string Status { get; set; }

        public required string ApplicantNIC { get; set; }
        public required string ApplicantNationality { get; set; }

        [ForeignKey("ApplicantNIC,ApplicantNationality")]
        public virtual Applicant Applicant { get; set; }
    }
}
