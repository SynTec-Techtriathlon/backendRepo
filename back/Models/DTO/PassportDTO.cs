namespace Back.Models.DTO
{
    public class PassportDTO
    {
        public required string Id { get; set; }
        public required DateTime DateOfExpire { get; set; }
        public required DateTime DateOfIssue { get; set; }
        public required string PassportImage { get; set; }

    }
}
