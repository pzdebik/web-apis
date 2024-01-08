using AutoMapper;
using FitnessTrackingSystem.Dto;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;
using FitnessTrackingSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTrackingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProgressTrackingController : ControllerBase
    {
        private readonly IProgressTrackingRepository _progressTrackingRepository;
        private readonly IMapper _mapper;

        public ProgressTrackingController(IProgressTrackingRepository progressTrackingRepository,
            IMapper mapper)
        {
            _progressTrackingRepository = progressTrackingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProgressTrackings()
        {
            var progressTrackings = _mapper.Map<List<ProgressTrackingDto>>(_progressTrackingRepository.GetAllProgressTrackings());
            return Ok(progressTrackings);
        }

        [HttpGet("{id}")]
        public IActionResult GetProgressTracking(int id)
        {
            var progressTracking = _mapper.Map<ProgressTrackingDto>(_progressTrackingRepository.GetProgressTracking(id));

            if (progressTracking == null)
            {
                return NotFound();
            }

            return Ok(progressTracking);
        }

        [HttpGet("progressTracking/{userId}")]
        public IActionResult GetAllProgressTrackingsByUserId(int userId)
        {
            var progressTrackings = _mapper.Map<List<ProgressTrackingDto>>(_progressTrackingRepository.GetAllProgressTrackingsByUserId(userId));
            return Ok(progressTrackings);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProgressTracking([FromBody] ProgressTrackingDto progressTrackingDto)
        {
            if (progressTrackingDto == null)
                return BadRequest(ModelState);

            var progressTracking = _progressTrackingRepository.GetAllProgressTrackings()
                .Where(c => c.Id == progressTrackingDto.Id)
                .FirstOrDefault();

            if (progressTracking != null)
            {
                ModelState.AddModelError("", "Nutrition plan already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var progressTrackingMap = _mapper.Map<ProgressTracking>(progressTrackingDto);

            if (!_progressTrackingRepository.CreateProgressTracking(progressTrackingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProgressTracking(int id, [FromBody] ProgressTrackingDto progressTracking)
        {
            if (progressTracking == null)
                return BadRequest(ModelState);

            if (id != progressTracking.Id)
                return BadRequest(ModelState);

            if (!_progressTrackingRepository.ProgressTrackingExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var progressTrackingMap = _mapper.Map<ProgressTracking>(progressTracking);

            if (!_progressTrackingRepository.UpdateProgressTracking(progressTrackingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}