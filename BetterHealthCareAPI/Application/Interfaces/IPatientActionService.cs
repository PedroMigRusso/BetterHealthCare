using BetterHealthCareAPI.Application.Dto;

namespace BetterHealthCareAPI.Application.Interfaces
{
    public interface IPatientActionService
    {
        Task<IEnumerable<PatientActionDto>> GetAllByPatientIdAsync(int patientId);
        Task<PatientActionDto?> GetByIdAsync(int patientId, int actionId);
        Task<PatientActionDto> CreateAsync(int patientId, CreatePatientActionDto dto);
        Task<bool> UpdateAsync(int patientId, int actionId, PatientActionDto dto);
        Task<bool> DeleteAsync(int patientId, int actionId);
    }
}
