using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Application
{
    public class PatientService : IPatientService
    {
        private readonly BetterHealthCareDbContext _context; // still needed for complex query
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(BetterHealthCareDbContext context,
                              IRepository<Patient> patientRepository,
                              IMapper mapper,
                              ILogger<PatientService> logger)
        {
            _context = context;
            _patientRepository = patientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDto>> GetAllAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return _mapper.Map<List<PatientDto>>(patients);
        }

        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<int> CreateAsync(PatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            await _patientRepository.AddAsync(patient);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created patient with id {Id}", patient.Id);
            return patient.Id;
        }

        public async Task<bool> UpdateAsync(int id, PatientDto dto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return false;
            _mapper.Map(dto, patient);
            _patientRepository.Update(patient);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated patient {Id}", id);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return false;
            _patientRepository.Remove(patient);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted patient {Id}", id);
            return true;
        }

        public async Task<PatientDto?> GetPatientWithActionsAsync(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.Actions)
                    .ThenInclude(pa => pa.Procedure) // inclui Procedure
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null) return null;

            var patientDto = new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                HealthNumber = patient.HealthNumber,
                DateOfBirth = patient.DateOfBirth,
                Actions = patient.Actions.Select(pa => new PatientActionDto
                {
                    Id = pa.Id,
                    DateOfProcedure = pa.DateOfProcedure,
                    FilesId = pa.FilesId,
                    Procedure = new ProcedureDto
                    {
                        Id = pa.Procedure!.Id,
                        Name = string.IsNullOrEmpty(pa.Procedure.Name) ? string.Empty : pa.Procedure.Name,
                        Type = string.IsNullOrEmpty(pa.Procedure.Type) ? string.Empty : pa.Procedure.Type
                    }
                }).ToList()
            };

            return patientDto;
        }
    }
}
