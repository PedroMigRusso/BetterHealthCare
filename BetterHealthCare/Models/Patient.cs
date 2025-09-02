namespace BetterHealthCare.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string HealthNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Action> Actions { get; set; } = new List<Action>();
    }
}
