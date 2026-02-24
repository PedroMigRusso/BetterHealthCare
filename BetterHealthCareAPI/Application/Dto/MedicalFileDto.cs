using System.ComponentModel.DataAnnotations;

namespace BetterHealthCareAPI.Application.Dto
{
    public class MedicalFileDto
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public string Base64 { get; set; } = null!;
    }
} 
