namespace Back.Models.DTO
{
    public class HistoryDTO
    {
        public required string VisaType { get; set; }
        public required DateTime VisaIssuedDate { get; set; }
        public required int VisaValidityPeriod { get; set; }
        public required DateTime DateLeaving { get; set; }
        public required string LastLocation { get; set; }
        public required string DepartureCountry { get; set; }
        public required DateTime ArrivalDate { get; set; }
        public required DateTime DepartureDate { get; set; }
    }
}
