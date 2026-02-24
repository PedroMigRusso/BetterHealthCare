using System.ComponentModel.DataAnnotations;

namespace BetterHealthCareAPI.Application.Dto
{
    public class CreatePatientActionDto
    {
        [Required]
        public int ProcedureId { get; set; }

        public List<int>? FilesId { get; set; }

        [Required]
        public DateTime DateOfProcedure { get; set; }
    }
} 
