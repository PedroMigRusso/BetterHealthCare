using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BetterHealthCareAPI.Application.Dto
{
    public class PatientActionDto
    {
        public int Id { get; set; }

        [Required]
        public ProcedureDto Procedure { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }

        public List<int>? FilesId { get; set; }

        [Required]
        public DateTime DateOfProcedure { get; set; }
    }
} 
