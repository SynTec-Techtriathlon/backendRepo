namespace Back.Models.DTO
{
    public class ApplicationDTO
    {
        public required string Purpose { get; set; }
        public required string Route { get; set; }
        public required string TravelMode { get; set; }
        public required DateTime ArrivalDate { get; set; }
        public required int Period { get; set; }
        public int AmountOfMoney { get; set; }
        public string? MoneyType { get; set; }
    }
}
