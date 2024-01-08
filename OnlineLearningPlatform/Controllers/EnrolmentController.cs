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
    public class EnrolmentController : ControllerBase
    {
        private readonly IEnrolmentRepository _enrolmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public EnrolmentController(IEnrolmentRepository enrolmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _enrolmentRepository = enrolmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Enrolment>))]
        public IActionResult GetAllEnrolments()
        {
            var enrolments = _mapper.Map<List<EnrolmentDto>>(_enrolmentRepository.GetAllEnrolments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(enrolments);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEnrolment([FromQuery] int courseId, [FromQuery] int studentId, [FromBody] EnrolmentDto enrolmentCreate)
        {
            // jesli nie ma nic w body zapytania
            if (enrolmentCreate == null)
                return BadRequest(ModelState);

            // wybieramy zapis po 2 id
            var enrolments = _enrolmentRepository.GetAllEnrolments()
                .Where(s => s.UserId == enrolmentCreate.UserId)
                .Where(c => c.CourseId == enrolmentCreate.CourseId)
                .FirstOrDefault();

            // jesli id są zajęte, czyli student już się zapisał na te zajęcia
            if (enrolments != null)
            {
                ModelState.AddModelError("", "Enrolment already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var enrolmentMap = _mapper.Map<Enrolment>(enrolmentCreate);

            enrolmentMap.User = _studentRepository.GetStudent(studentId);
            enrolmentMap.Course = _courseRepository.GetCourse(courseId);


            if (!_enrolmentRepository.CreateEnrolment(enrolmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
