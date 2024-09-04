using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models.DTO
{
    public class SpouseDTO
    {
        public required string SpouseNIC { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
    }
}
