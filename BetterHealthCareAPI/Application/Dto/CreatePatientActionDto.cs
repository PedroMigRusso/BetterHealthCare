namespace BetterHealthCareAPI.Application.Dto
{
    public class CreatePatientActionDto
    {
        public int ProcedureId { get; set; }
        public List<int>? FilesId { get; set; }
        public DateTime DateOfProcedure { get; set; }
    }
}
