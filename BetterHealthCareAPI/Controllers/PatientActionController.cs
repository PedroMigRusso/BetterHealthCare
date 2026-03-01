using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Controllers
{
    /// <summary>
    /// Patient action/procedure management endpoints
    /// </summary>
    [ApiController]
    [Route("api/patients/{patientId}/actions")]
    public class PatientActionController : ControllerBase
    {
        private readonly IPatientActionService _service;
        private readonly ILogger<PatientActionController> _logger;

        public PatientActionController(IPatientActionService service, ILogger<PatientActionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all actions for a specific patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>List of all actions for the patient</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllActionsForPatient(int patientId)
        {
            var actions = await _service.GetAllByPatientIdAsync(patientId);
            return Ok(actions);
        }

        /// <summary>
        /// Retrieves a specific action for a patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="actionId">Action ID</param>
        /// <returns>Action details or NotFound</returns>
        [HttpGet("{actionId}")]
        public async Task<IActionResult> GetActionById(int patientId, int actionId)
        {
            var action = await _service.GetByIdAsync(patientId, actionId);
            if (action == null) return NotFound();
            return Ok(action);
        }

        /// <summary>
        /// Creates a new action/procedure for a patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="dto">Action data to create</param>
        /// <returns>Created action with new ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAction(int patientId, [FromBody] CreatePatientActionDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create patient action invalid for patient {PatientId}: {Errors}", patientId, ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var createdAction = await _service.CreateAsync(patientId, dto);
            return CreatedAtAction(nameof(GetActionById), new { patientId, actionId = createdAction.Id }, createdAction);
        }

        /// <summary>
        /// Updates an existing action for a patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="actionId">Action ID to update</param>
        /// <param name="dto">Updated action data</param>
        /// <returns>NoContent if successful, NotFound if action doesn't exist</returns>
        [HttpPut("{actionId}")]
        public async Task<IActionResult> UpdateAction(int patientId, int actionId, [FromBody] PatientActionDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update patient action invalid for patient {PatientId} action {ActionId}: {Errors}", patientId, actionId, ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var updated = await _service.UpdateAsync(patientId, actionId, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes an action for a patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="actionId">Action ID to delete</param>
        /// <returns>NoContent if successful, NotFound if action doesn't exist</returns>
        [HttpDelete("{actionId}")]
        public async Task<IActionResult> DeleteAction(int patientId, int actionId)
        {
            var deleted = await _service.DeleteAsync(patientId, actionId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
