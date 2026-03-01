using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterHealthCareAPI.Controllers
{
    /// <summary>
    /// Patient management endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService service, ILogger<PatientsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all patients
        /// </summary>
        /// <returns>List of all patients</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _service.GetAllAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Retrieves a specific patient by ID
        /// </summary>
        /// <param name="id">The patient ID</param>
        /// <returns>Patient details or NotFound if patient doesn't exist</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        /// <summary>
        /// Creates a new patient
        /// </summary>
        /// <param name="patient">Patient data to create</param>
        /// <returns>Created patient with new ID</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Patient create request invalid: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var id = await _service.CreateAsync(patient);
            return CreatedAtAction(nameof(GetById), new { id }, patient);
        }

        /// <summary>
        /// Updates an existing patient
        /// </summary>
        /// <param name="id">Patient ID to update</param>
        /// <param name="patient">Updated patient data</param>
        /// <returns>NoContent if successful, NotFound if patient doesn't exist</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto patient)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Patient update request invalid for id {Id}: {Errors}", id, ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            var success = await _service.UpdateAsync(id, patient);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a patient
        /// </summary>
        /// <param name="id">Patient ID to delete</param>
        /// <returns>NoContent if successful, NotFound if patient doesn't exist</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Retrieves a patient with all associated actions/procedures
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Patient details including all associated actions</returns>
        [HttpGet("{id}/full")]
        public async Task<IActionResult> GetPatientWithActions(int id)
        {
            var patientDto = await _service.GetPatientWithActionsAsync(id);
            if (patientDto == null) return NotFound("Patient not found");
            return Ok(patientDto);
        }
    }
}
