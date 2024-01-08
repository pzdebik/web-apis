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
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IMapper _mapper;

        public AssessmentController(IAssessmentRepository assessmentRepository,
            IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Assessment>))]
        public IActionResult GetAllAssignments()
        {
            var assessments = _mapper.Map<List<AssessmentDto>>(_assessmentRepository.GetAllAssessments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(assessments);
        }

        [HttpGet("{assessmentId}")]
        [ProducesResponseType(200, Type = typeof(Assessment))]
        [ProducesResponseType(400)]
        public IActionResult GetAssessment(int assessmentId)
        {
            if (!_assessmentRepository.AssessmentExists(assessmentId))
                return NotFound();

            var assessment = _mapper.Map<AssessmentDto>(_assessmentRepository.GetAssessment(assessmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(assessment);
        }

        [HttpGet("studentAssessments/{studId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Assessment>))]
        [ProducesResponseType(400)]

        public IActionResult GetAssessmentsByStudent(int studId)
        {
            if (!_assessmentRepository.AssessmentExists(studId))
                return NotFound();

            var studAssessments = _mapper.Map<List<AssessmentDto>>(_assessmentRepository.GetAssessmentsByStudent(studId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(studAssessments);
        }
        
    }
}
