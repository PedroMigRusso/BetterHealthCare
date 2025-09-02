using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Domain.Models;

namespace BetterHealthCareAPI.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<PatientDto> GetByIdAsync(int id);
        Task<int> CreateAsync(PatientDto dto);
        Task<bool> UpdateAsync(int id, PatientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
