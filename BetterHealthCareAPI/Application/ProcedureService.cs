using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Application
{
    public class ProcedureService : IProcedureService
    {
        private readonly BetterHealthCareDbContext _context;
        private readonly IRepository<Procedure> _procedureRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProcedureService> _logger;

        public ProcedureService(BetterHealthCareDbContext context,
                                IRepository<Procedure> procedureRepository,
                                IMapper mapper,
                                ILogger<ProcedureService> logger)
        {
            _context = context;
            _procedureRepository = procedureRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProcedureDto>> GetAllAsync()
        {
            var procedures = await _procedureRepository.GetAllAsync();
            return _mapper.Map<List<ProcedureDto>>(procedures);
        }

        public async Task<ProcedureDto?> GetByIdAsync(int id)
        {
            var procedure = await _procedureRepository.GetByIdAsync(id);
            return _mapper.Map<ProcedureDto>(procedure);
        }

        public async Task<int> CreateAsync(ProcedureDto dto)
        {
            var procedure = _mapper.Map<Procedure>(dto);
            await _procedureRepository.AddAsync(procedure);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created procedure {Id}", procedure.Id);
            return procedure.Id;
        }

        public async Task<bool> UpdateAsync(int id, ProcedureDto dto)
        {
            var procedure = await _procedureRepository.GetByIdAsync(id);
            if (procedure == null) return false;
            _mapper.Map(dto, procedure);
            _procedureRepository.Update(procedure);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated procedure {Id}", id);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var procedure = await _procedureRepository.GetByIdAsync(id);
            if (procedure == null) return false;
            _procedureRepository.Remove(procedure);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted procedure {Id}", id);
            return true;
        }
    }
}
