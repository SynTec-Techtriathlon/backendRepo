using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Back.Models
{
    public class Passport
    {
        public required string Id { get; set; }
        public required string Country { get; set; }
        public required DateTime DateOfExpire { get; set; }
        public required DateTime DateOfIssue { get; set; }

        public required string ApplicantNIC {  get; set; }
        public required string ApplicantNationality { get; set; }

        [ForeignKey("ApplicantNIC,ApplicantNationality")]
        public virtual Applicant Applicant { get; set; }
    }

}
