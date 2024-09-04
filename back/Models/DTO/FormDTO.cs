namespace Back.Models.DTO
{
    public class FormDTO
    {
        public required ApplicantDTO Applicant { get; set; }
        public required ApplicationDTO Application { get; set; }
        public required PassportDTO Passport { get; set; }
        public SpouseDTO? Spouse { get; set; }
        public ICollection<HistoryDTO> History { get; set; }

    }
}
