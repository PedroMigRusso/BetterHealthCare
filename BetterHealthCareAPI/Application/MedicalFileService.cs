using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Application
{
    public class MedicalFileService : IMedicalFileService
    {
        private readonly BetterHealthCareDbContext _context;
        private readonly IRepository<MedicalFile> _fileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalFileService> _logger;

        public MedicalFileService(BetterHealthCareDbContext context,
                                  IRepository<MedicalFile> fileRepository,
                                  IMapper mapper,
                                  ILogger<MedicalFileService> logger)
        {
            _context = context;
            _fileRepository = fileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MedicalFileDto>> GetAllAsync()
        {
            var files = await _fileRepository.GetAllAsync();
            return _mapper.Map<List<MedicalFileDto>>(files);
        }

        public async Task<MedicalFileDto> GetByIdAsync(int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            return _mapper.Map<MedicalFileDto>(file);
        }

        public async Task<MedicalFileDto> CreateAsync(MedicalFileDto dto)
        {
            var entity = _mapper.Map<MedicalFile>(dto);
            await _fileRepository.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created medical file {Id}", entity.Id);
            return _mapper.Map<MedicalFileDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, MedicalFileDto dto)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null) return false;
            _mapper.Map(dto, file);
            _fileRepository.Update(file);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated medical file {Id}", id);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null) return false;
            _fileRepository.Remove(file);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted medical file {Id}", id);
            return true;
        }

    }

}
