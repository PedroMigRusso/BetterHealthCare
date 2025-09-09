using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Domain.Models;

namespace BetterHealthCareAPI.Application.Interfaces
{
    public interface IMedicalFileService
    {
        Task<IEnumerable<MedicalFile>> GetAllAsync();
        Task<MedicalFileDto> GetByIdAsync(int id);
        Task<MedicalFileDto> CreateAsync(MedicalFileDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, MedicalFileDto dto);
    }
}
