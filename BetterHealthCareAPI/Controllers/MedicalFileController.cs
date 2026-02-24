using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class MedicalFileController : ControllerBase // <- use ControllerBase
    {
        private readonly IMedicalFileService _service;
        private readonly ILogger<MedicalFileController> _logger;

        public MedicalFileController(IMedicalFileService service, ILogger<MedicalFileController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var files = await _service.GetAllAsync();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var file = await _service.GetByIdAsync(id);
            if (file == null)
                return NotFound();

            return Ok(file);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalFileDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Medical file create invalid: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalFileDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Medical file update invalid for id {Id}: {Errors}", id, ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
