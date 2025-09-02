namespace BetterHealthCareAPI.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HealthNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<PatientAction> Actions { get; set; } = new List<PatientAction>();
    }
}
