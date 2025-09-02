using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BetterHealthCareAPI.Application
{
    public class MedicalFileService : IMedicalFileService
    {
        private readonly BetterHealthCareDbContext _context;
        private readonly IMapper _mapper;

        public MedicalFileService(BetterHealthCareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalFile>> GetAllAsync()
        {
            var files = await _context.MedicalFiles.ToListAsync();
            return files;
        }

        public async Task<MedicalFileDto> GetByIdAsync(int id)
        {
            var file = await _context.MedicalFiles.FindAsync(id);
            return _mapper.Map<MedicalFileDto>(file);
        }

        public async Task<int> CreateAsync(MedicalFileDto dto)
        {
            var file = _mapper.Map<MedicalFile>(dto);
            _context.MedicalFiles.Add(file);
            await _context.SaveChangesAsync();
            return file.Id;
        }

        public async Task<bool> UpdateAsync(int id, MedicalFileDto dto)
        {
            var file = await _context.MedicalFiles.FindAsync(id);
            if (file == null) return false;
            _mapper.Map(dto, file);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var file = await _context.MedicalFiles.FindAsync(id);
            if (file == null) return false;
            _context.MedicalFiles.Remove(file);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
