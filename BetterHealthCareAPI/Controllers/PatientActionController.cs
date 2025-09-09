using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BetterHealthCareAPI.Controllers
{
    [ApiController]
    [Route("api/patients/{patientId}/actions")]
    public class PatientActionController : ControllerBase
    {
        private readonly IPatientActionService _service;

        public PatientActionController(IPatientActionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActionsForPatient(int patientId)
        {
            var actions = await _service.GetAllByPatientIdAsync(patientId);
            return Ok(actions);
        }

        [HttpGet("{actionId}")]
        public async Task<IActionResult> GetActionById(int patientId, int actionId)
        {
            var action = await _service.GetByIdAsync(patientId, actionId);
            if (action == null) return NotFound();
            return Ok(action);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAction(int patientId, [FromBody] CreatePatientActionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdAction = await _service.CreateAsync(patientId, dto);
            return CreatedAtAction(nameof(GetActionById), new { patientId, actionId = createdAction.Id }, createdAction);
        }

        [HttpPut("{actionId}")]
        public async Task<IActionResult> UpdateAction(int patientId, int actionId, [FromBody] PatientActionDto dto)
        {
            var updated = await _service.UpdateAsync(patientId, actionId, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{actionId}")]
        public async Task<IActionResult> DeleteAction(int patientId, int actionId)
        {
            var deleted = await _service.DeleteAsync(patientId, actionId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
