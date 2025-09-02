using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Domain.Models;

namespace BetterHealthCareAPI.Application.Interfaces
{
    public interface IProcedureService
    {
        Task<IEnumerable<Procedure>> GetAllAsync();
        Task<ProcedureDto> GetByIdAsync(int id);
        Task<int> CreateAsync(ProcedureDto dto);
        Task<bool> UpdateAsync(int id, ProcedureDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
