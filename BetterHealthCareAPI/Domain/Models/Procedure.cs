namespace BetterHealthCareAPI.Domain.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; } // "Operation" or "Analysis"
    }
}
