namespace BetterHealthCareAPI.Domain.Models
{
    public class MedicalFile
    {
        public int Id { get; set; }
        public string? Base64 { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
