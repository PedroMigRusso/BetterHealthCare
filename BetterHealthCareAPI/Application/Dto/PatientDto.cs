using BetterHealthCareAPI.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BetterHealthCareAPI.Application.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string HealthNumber { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfCreation { get; set; }

        public ICollection<PatientActionDto> Actions { get; set; } = new List<PatientActionDto>();
    }
} 
