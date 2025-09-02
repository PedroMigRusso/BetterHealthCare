using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BetterHealthCareAPI.Application
{
    public class ProcedureService : IProcedureService
    {
        private readonly BetterHealthCareDbContext _context;
        private readonly IMapper _mapper;

        public ProcedureService(BetterHealthCareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Procedure>> GetAllAsync()
        {
            var procedures = await _context.Procedures.ToListAsync();
            return procedures;
        }

        public async Task<ProcedureDto?> GetByIdAsync(int id)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            return _mapper.Map<ProcedureDto>(procedure);
        }

        public async Task<int> CreateAsync(ProcedureDto dto)
        {
            var procedure = _mapper.Map<Procedure>(dto);
            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();
            return procedure.Id;
        }

        public async Task<bool> UpdateAsync(int id, ProcedureDto dto)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null) return false;
            _mapper.Map(dto, procedure);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null) return false;
            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
