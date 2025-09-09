namespace BetterHealthCareAPI.Application.Dto
{
    public class ProcedureDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
    }
}
