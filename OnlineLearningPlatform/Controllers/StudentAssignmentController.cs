using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Dto;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;
using OnlineLearningPlatform.Repository;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentAssignmentController : ControllerBase
    {
        private readonly IStudentAssignmentRepository _studentAssignmentRepository;
        private readonly IMapper _mapper;

        public StudentAssignmentController(IStudentAssignmentRepository studentAssignmentRepository,
            IMapper mapper)
        {
            _studentAssignmentRepository = studentAssignmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentAssignment>))]
        public IActionResult GetAllStudentAssignments()
        {
            var studentAssignments = _mapper.Map<List<StudentAssignmentDto>>(_studentAssignmentRepository.GetAllStudentAssignments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(studentAssignments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(StudentAssignment))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentAssignment(int id)
        {
            if (!_studentAssignmentRepository.StudentAssignmentExists(id))
                return NotFound();

            var studentAssignment = _mapper.Map<StudentAssignmentDto>(_studentAssignmentRepository.GetStudentAssignment(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(studentAssignment);
        }

        [HttpGet("studentAssignments/{studId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentAssignment>))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentAssignmentsByStudentId(int studId)
        {
            if (!_studentAssignmentRepository.StudentAssignmentExists(studId))
                return NotFound();

            var studentAssignments = _mapper.Map<List<StudentAssignmentDto>>(_studentAssignmentRepository.GetStudentAssignmentsByStudentId(studId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(studentAssignments);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudentAssignment(int id, [FromBody] StudentAssignmentDto updatedStudentAssignment)
        {
            if (updatedStudentAssignment == null)
                return BadRequest(ModelState);

            if (id != updatedStudentAssignment.Id)
                return BadRequest(ModelState);

            if (!_studentAssignmentRepository.StudentAssignmentExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var studentAssignmentMap = _mapper.Map<StudentAssignment>(updatedStudentAssignment);

            if (!_studentAssignmentRepository.UpdateStudentAssignment(studentAssignmentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating student's assignment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
