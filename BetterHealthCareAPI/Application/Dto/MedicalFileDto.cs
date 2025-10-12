namespace BetterHealthCareAPI.Application.Dto
{
    public class MedicalFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public required string Base64 { get; set; }
    }
}
