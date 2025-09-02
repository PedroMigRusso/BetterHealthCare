using Microsoft.AspNetCore.Mvc;

namespace BetterHealthCareAPI.Application.Dto
{
    public class PatientActionDto
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public List<int>? FilesId { get; set; }
        public DateTime DateOfProcedure { get; set; }
    }
}
