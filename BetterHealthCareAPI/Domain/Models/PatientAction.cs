namespace BetterHealthCareAPI.Domain.Models
{
    public class PatientAction
    {
        public int Id { get; set; }

        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; }

        public List<int> FilesId { get; set; } = new List<int>(); 

        public DateTime DateOfProcedure { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
