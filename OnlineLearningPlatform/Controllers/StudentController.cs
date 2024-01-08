using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Dto;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAllStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetAllStudents());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(students);
        }

        [HttpGet("{studId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studId) 
        {
            if (!_studentRepository.StudentExists(studId))
                return NotFound();

            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studId));

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(student);
        }

        [HttpGet("{studId}/avgScore")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentAvgScore(int studId)
        {
            if (!_studentRepository.StudentExists(studId))
                return NotFound();

            var score = _studentRepository.GetStudentAvgScore(studId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(score);
        }

        [HttpGet("assessments/{studId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentAssessments(int studId)
        {
            if (!_studentRepository.StudentExists(studId))
                return NotFound();

            var studentAssessments = _mapper.Map<List<AssessmentDto>>(_studentRepository.GetStudentAssessments(studId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(studentAssessments);
        }
        
    }
}
