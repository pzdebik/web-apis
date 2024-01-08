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
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly IMapper _mapper;

        public RoutineController(IRoutineRepository routineRepository,
            IMapper mapper)
        {
            _routineRepository = routineRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRoutines()
        {
            var routines = _mapper.Map<List<RoutineDto>>(_routineRepository.GetAllRoutines());
            return Ok(routines);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoutine(int id)
        {
            var routine = _mapper.Map<RoutineDto>(_routineRepository.GetRoutine(id));

            if (routine == null)
            {
                return NotFound();
            }

            return Ok(routine);
        }

        [HttpGet("routine/{userId}")]
        public IActionResult GetAllRoutinesByUserId(int userId)
        {
            var routines = _mapper.Map<List<RoutineDto>>(_routineRepository.GetAllRoutinesByUserId(userId));
            return Ok(routines);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRoutine([FromBody] RoutineDto routineDto)
        {
            if (routineDto == null)
                return BadRequest(ModelState);

            var routine = _routineRepository.GetAllRoutines()
                .Where(c => c.Name.Trim().ToUpper() == routineDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (routine != null)
            {
                ModelState.AddModelError("", "Routine already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routineMap = _mapper.Map<Routine>(routineDto);

            if (!_routineRepository.CreateRoutine(routineMap))
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
        public IActionResult UpdateRoutine(int id, [FromBody] RoutineDto routine)
        {
            if (routine == null)
                return BadRequest(ModelState);

            if (id != routine.Id)
                return BadRequest(ModelState);

            if (!_routineRepository.RoutineExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var routineMap = _mapper.Map<Routine>(routine);

            if (!_routineRepository.UpdateRoutine(routineMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}