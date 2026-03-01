using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Controllers
{
    /// <summary>
    /// Medical file management endpoints
    /// </summary>
    [ApiController]
    [Route("api/files")]
    public class MedicalFileController : ControllerBase
    {
        private readonly IMedicalFileService _service;
        private readonly ILogger<MedicalFileController> _logger;

        public MedicalFileController(IMedicalFileService service, ILogger<MedicalFileController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all medical files
        /// </summary>
        /// <returns>List of all medical files</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var files = await _service.GetAllAsync();
            return Ok(files);
        }

        /// <summary>
        /// Retrieves a specific medical file by ID
        /// </summary>
        /// <param name="id">Medical file ID</param>
        /// <returns>Medical file details or NotFound</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var file = await _service.GetByIdAsync(id);
            if (file == null)
                return NotFound();

            return Ok(file);
        }

        /// <summary>
        /// Creates a new medical file
        /// </summary>
        /// <param name="dto">Medical file data to create</param>
        /// <returns>Created medical file</returns>
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

        /// <summary>
        /// Updates an existing medical file
        /// </summary>
        /// <param name="id">Medical file ID to update</param>
        /// <param name="dto">Updated medical file data</param>
        /// <returns>NoContent if successful, NotFound if file doesn't exist</returns>
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

        /// <summary>
        /// Deletes a medical file
        /// </summary>
        /// <param name="id">Medical file ID to delete</param>
        /// <returns>NoContent if successful, NotFound if file doesn't exist</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
