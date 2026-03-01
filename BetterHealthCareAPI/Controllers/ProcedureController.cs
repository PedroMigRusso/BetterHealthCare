using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Controllers
{
    /// <summary>
    /// Procedure/Treatment management endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureService _service;
        private readonly ILogger<ProcedureController> _logger;

        public ProcedureController(IProcedureService service, ILogger<ProcedureController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all procedures
        /// </summary>
        /// <returns>List of all procedures</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var procedures = await _service.GetAllAsync();
            return Ok(procedures);
        }

        /// <summary>
        /// Retrieves a specific procedure by ID
        /// </summary>
        /// <param name="id">Procedure ID</param>
        /// <returns>Procedure details or NotFound</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var procedure = await _service.GetByIdAsync(id);
            if (procedure == null) return NotFound();
            return Ok(procedure);
        }

        /// <summary>
        /// Creates a new procedure
        /// </summary>
        /// <param name="dto">Procedure data to create</param>
        /// <returns>Created procedure with new ID</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProcedureDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Procedure create invalid: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        /// <summary>
        /// Updates an existing procedure
        /// </summary>
        /// <param name="id">Procedure ID to update</param>
        /// <param name="dto">Updated procedure data</param>
        /// <returns>NoContent if successful, NotFound if procedure doesn't exist</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProcedureDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Procedure update invalid for id {Id}: {Errors}", id, ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a procedure
        /// </summary>
        /// <param name="id">Procedure ID to delete</param>
        /// <returns>NoContent if successful, NotFound if procedure doesn't exist</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
