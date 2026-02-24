using System.ComponentModel.DataAnnotations;

namespace BetterHealthCareAPI.Application.Dto
{
    public class ProcedureDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = null!;
    }
} 
