namespace Back.Models.DTO
{
    public class ApplicantDTO
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
    }
}
