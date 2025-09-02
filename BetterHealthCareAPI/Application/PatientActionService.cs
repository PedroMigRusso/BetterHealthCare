using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BetterHealthCareAPI.Application
{
    public class PatientActionService : IPatientActionService
    {
        private readonly BetterHealthCareDbContext _context;
        private readonly IMapper _mapper;

        public PatientActionService(BetterHealthCareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientActionDto>> GetAllByPatientIdAsync(int patientId)
        {
            var actions = await _context.PatientActions
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PatientActionDto>>(actions);
        }

        public async Task<PatientActionDto?> GetByIdAsync(int patientId, int actionId)
        {
            var action = await _context.PatientActions
                .FirstOrDefaultAsync(a => a.Id == actionId && a.PatientId == patientId);

            return action == null ? null : _mapper.Map<PatientActionDto>(action);
        }

        public async Task<PatientActionDto> CreateAsync(int patientId, PatientActionDto dto)
        {
            var action = _mapper.Map<PatientAction>(dto);
            action.PatientId = patientId;

            _context.PatientActions.Add(action);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientActionDto>(action);
        }

        public async Task<bool> UpdateAsync(int patientId, int actionId, PatientActionDto dto)
        {
            var action = await _context.PatientActions
                .FirstOrDefaultAsync(a => a.Id == actionId && a.PatientId == patientId);

            if (action == null) return false;

            _mapper.Map(dto, action); // Atualiza os campos
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int patientId, int actionId)
        {
            var action = await _context.PatientActions
                .FirstOrDefaultAsync(a => a.Id == actionId && a.PatientId == patientId);

            if (action == null) return false;

            _context.PatientActions.Remove(action);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
