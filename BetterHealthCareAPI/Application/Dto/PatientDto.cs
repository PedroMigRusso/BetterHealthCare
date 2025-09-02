using BetterHealthCareAPI.Domain.Models;

namespace BetterHealthCareAPI.Application.Dto
{
    public class PatientDto
    {
        public required string Name { get; set; }
        public required string HealthNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<PatientActionDto> Actions { get; set; } = new List<PatientActionDto>();
    }
}
