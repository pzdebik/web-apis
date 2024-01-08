using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Dto;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;
using OnlineLearningPlatform.Repository;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;
        public InstructorController(IInstructorRepository instructorRepository,
            IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Instructor>))]
        public IActionResult GetAllInstructors()
        {
            var instructors = _mapper.Map<List<InstructorDto>>(_instructorRepository.GetAllInstructors());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(instructors);
        }

        [HttpGet("{instructorId}")]
        [ProducesResponseType(200, Type = typeof(Instructor))]
        [ProducesResponseType(400)]
        public IActionResult GetInstructor(int instructorId)
        {
            if (!_instructorRepository.InstructorExists(instructorId))
                return NotFound();

            var instructor = _mapper.Map<InstructorDto>(_instructorRepository.GetInstructor(instructorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(instructor);
        }
    }
}
