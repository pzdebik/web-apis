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
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        public AssignmentController(IAssignmentRepository assignmentRepository,
            IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Assignment>))]
        public IActionResult GetAllAssignments()
        {
            var assignments = _mapper.Map<List<AssignmentDto>>(_assignmentRepository.GetAllAssignments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(assignments);
        }

        [HttpGet("{assignmentId}")]
        [ProducesResponseType(200, Type = typeof(Assignment))]
        [ProducesResponseType(400)]
        public IActionResult GetAssignment(int assignmentId)
        {
            if (!_assignmentRepository.AssignmentExists(assignmentId))
                return NotFound();

            var assignment = _mapper.Map<AssignmentDto>(_assignmentRepository.GetAssignment(assignmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(assignment);
        }

        [HttpGet("assessments/{assignmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Assignment>))]
        [ProducesResponseType(400)]

        public IActionResult GetAssessmentsFromAssignment(int assignmentId)
        {
            if (!_assignmentRepository.AssignmentExists(assignmentId))
                return NotFound();

            var assessments = _mapper.Map<List<AssessmentDto>>(_assignmentRepository.GetAssessmentsFromAssignment(assignmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(assessments);
        }
    }
}
