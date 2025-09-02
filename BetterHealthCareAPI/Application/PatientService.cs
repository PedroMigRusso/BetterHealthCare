using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BetterHealthCareAPI.Application
{
    public class PatientService : IPatientService
    {
        private readonly BetterHealthCareDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(BetterHealthCareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<int> CreateAsync(PatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient.Id;
        }

        public async Task<bool> UpdateAsync(int id, PatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;
            _mapper.Map(dto, patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
