using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var procedures = await _service.GetAllAsync();
            return Ok(procedures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var procedure = await _service.GetByIdAsync(id);
            if (procedure == null) return NotFound();
            return Ok(procedure);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
