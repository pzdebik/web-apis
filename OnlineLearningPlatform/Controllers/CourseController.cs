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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetAllCourses()
        {
            var courses = _mapper.Map<List<CourseDto>>(_courseRepository.GetAllCourses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult GetCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var course = _mapper.Map<CourseDto>(_courseRepository.GetCourse(courseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(course);
        }

        [HttpGet("assignments/{courseId}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]

        public IActionResult GetAssignmentsFromCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var courseAssignments = _courseRepository.GetAssignmentsFromCourse(courseId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(courseAssignments);
        }

    }
}
