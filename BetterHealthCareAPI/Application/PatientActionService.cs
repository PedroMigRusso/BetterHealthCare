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
                .Include(a => a.Procedure) // <-- IMPORTANTE
                .ToListAsync();

            var dto = actions.Select(a => new PatientActionDto
            {
                Id = a.Id,
                DateOfProcedure = a.DateOfProcedure,
                FilesId = a.FilesId,
                Procedure = new ProcedureDto
                {
                    Id = a.Procedure.Id,
                    Name = a.Procedure.Name ?? string.Empty,
                    Type = a.Procedure.Type ?? string.Empty
                }
            }).ToList();

            return dto;
        }

        public async Task<PatientActionDto?> GetByIdAsync(int patientId, int actionId)
        {
            var action = await _context.PatientActions
                .Include(a => a.Procedure) 
                .FirstOrDefaultAsync(a => a.Id == actionId && a.PatientId == patientId);

            if (action == null)
                return null;

            return new PatientActionDto
            {
                Id = action.Id,
                DateOfProcedure = action.DateOfProcedure,
                FilesId = action.FilesId,
                PatientId = action.PatientId,
                Procedure = new ProcedureDto
                {
                    Id = action.Procedure?.Id ?? 0,
                    Name = action.Procedure?.Name ?? string.Empty,
                    Type = action.Procedure?.Type ?? string.Empty
                }
            };
        }

        public async Task<PatientActionDto> CreateAsync(int patientId, CreatePatientActionDto dto)
        {
            var entity = new PatientAction
            {
                PatientId = patientId,
                ProcedureId = dto.ProcedureId,
                DateOfProcedure = dto.DateOfProcedure,
                FilesId = dto.FilesId ?? new List<int>()
            };

            _context.PatientActions.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Procedure).LoadAsync();

            return new PatientActionDto
            {
                Id = entity.Id,
                DateOfProcedure = entity.DateOfProcedure,
                FilesId = entity.FilesId,
                Procedure = new ProcedureDto
                {
                    Id = entity.Procedure.Id,
                    Name = entity.Procedure.Name ?? string.Empty,
                    Type = entity.Procedure.Type ?? string.Empty
                }
            };
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
