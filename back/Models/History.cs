using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        public required string VisaType { get; set; }
        public DateTime VisaIssuedDate { get; set; }
        public int VisaValidityPeriod { get; set; }
        public DateTime DateLeaving { get; set; }
        public required string LastLocation { get; set; }


        [ForeignKey("ApplicantNIC,ApplicantNationality")]
        public required string ApplicantNIC { get; set; }
        public required string ApplicantNationality { get; set; }


        public virtual Applicant Applicant { get; set; }
    }
}
