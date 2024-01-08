using AutoMapper;
using FitnessTrackingSystem.Dto;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;
using FitnessTrackingSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FitnessTrackingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExcerciseController : ControllerBase
    {
        private readonly IExcerciseRepository _excerciseRepository;
        private readonly IMapper _mapper;

        public ExcerciseController(IExcerciseRepository excerciseRepository,
            IMapper mapper)
        {
            _excerciseRepository = excerciseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllExcercises()
        {
            var excercises = _mapper.Map<List<ExcerciseDto>>(_excerciseRepository.GetAllExcercises());
            return Ok(excercises);
        }

        [HttpGet("{id}")]
        public IActionResult GetExcercise(int id)
        {
            var excercise = _mapper.Map<ExcerciseDto>(_excerciseRepository.GetExcercise(id));

            if (excercise == null)
            {
                return NotFound();
            }

            return Ok(excercise);
        }

        [HttpGet("excerciseRoutine/{routineId}")]
        public IActionResult GetAllExcercisesByRoutineId(int routineId)
        {
            var excercises = _mapper.Map<List<ExcerciseRoutineDto>>(_excerciseRepository.GetAllExcercisesByRoutineId(routineId));
            return Ok(excercises);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateExcercise([FromBody] ExcerciseDto excerciseDto)
        {
            if (excerciseDto == null)
                return BadRequest(ModelState);

            var excercise = _excerciseRepository.GetAllExcercises()
                .Where(c => c.Name.Trim().ToUpper() == excerciseDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (excercise != null)
            {
                ModelState.AddModelError("", "Excercise already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var excerciseMap = _mapper.Map<Excercise>(excerciseDto);

            if (!_excerciseRepository.CreateExcercise(excerciseMap))
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
        public IActionResult UpdateExcercise(int id, [FromBody] ExcerciseDto excercise)
        {
            if (excercise == null)
                return BadRequest(ModelState);

            if (id != excercise.Id)
                return BadRequest(ModelState);

            if (!_excerciseRepository.ExcerciseExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var excerciseMap = _mapper.Map<Excercise>(excercise);

            if (!_excerciseRepository.UpdateExcercise(excerciseMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}