namespace BetterHealthCareAPI.Application.Dto
{
    public class MedicalFileDto
    {
        public int Id { get; set; }
        public required string Base64 { get; set; }
    }
}
