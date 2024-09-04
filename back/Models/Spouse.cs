using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Spouse
    {
        public required string ApplicantNIC { get; set; }
        public required string ApplicantNationality { get; set; }
        public required string SpouseNIC { get; set; }

        public required string Name { get; set; }
        public string Address { get; set; }

        [ForeignKey("ApplicantNIC,ApplicantNationality")]
        public virtual Applicant Applicant { get; set; }
    }

}
