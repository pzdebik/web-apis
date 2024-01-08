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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public WorkoutController(IWorkoutRepository workoutRepository,
            IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllWorkouts()
        {
            var workouts = _mapper.Map<List<WorkoutDto>>(_workoutRepository.GetAllWorkouts());
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkout(int id)
        {
            var workout = _mapper.Map<WorkoutDto>(_workoutRepository.GetWorkout(id));

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        [HttpGet("workout/{userId}")]
        public IActionResult GetAllWorkoutsByUserId(int userId)
        {
            var workouts = _mapper.Map<List<WorkoutDto>>(_workoutRepository.GetAllWorkoutsByUserId(userId));
            return Ok(workouts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWorkout([FromBody] WorkoutDto workoutDto)
        {
            if (workoutDto == null)
                return BadRequest(ModelState);

            var workout = _workoutRepository.GetAllWorkouts()
                .Where(c => c.Name.Trim().ToUpper() == workoutDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (workout != null)
            {
                ModelState.AddModelError("", "Workout already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var workoutMap = _mapper.Map<Workout>(workoutDto);

            if (!_workoutRepository.CreateWorkout(workoutMap))
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
        public IActionResult UpdateWorkout(int id, [FromBody] WorkoutDto workout)
        {
            if (workout == null)
                return BadRequest(ModelState);

            if (id != workout.Id)
                return BadRequest(ModelState);

            if (!_workoutRepository.WorkoutExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var workoutMap = _mapper.Map<Workout>(workout);

            if (!_workoutRepository.UpdateWorkout(workoutMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}