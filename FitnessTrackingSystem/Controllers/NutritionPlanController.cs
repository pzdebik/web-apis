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
    public class NutritionPlanController : ControllerBase
    {
        private readonly INutritionPlanRepository _nutritionPlanRepository;
        private readonly IMapper _mapper;

        public NutritionPlanController(INutritionPlanRepository nutritionPlanRepository,
            IMapper mapper)
        {
            _nutritionPlanRepository = nutritionPlanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllNutritionPlans()
        {
            var nutritionPlans = _mapper.Map<List<NutritionPlanDto>>(_nutritionPlanRepository.GetAllNutritionPlans());
            return Ok(nutritionPlans);
        }

        [HttpGet("{id}")]
        public IActionResult GetNutritionPlan(int id)
        {
            var nutritionPlan = _mapper.Map<NutritionPlanDto>(_nutritionPlanRepository.GetNutritionPlan(id));

            if (nutritionPlan == null)
            {
                return NotFound();
            }

            return Ok(nutritionPlan);
        }

        [HttpGet("nutritionPlan/{userId}")]
        public IActionResult GetAllNutritionPlansByUserId(int userId)
        {
            var nutritionPlans = _mapper.Map<List<NutritionPlanDto>>(_nutritionPlanRepository.GetAllNutritionPlansByUserId(userId));

            if (nutritionPlans == null)
            {
                return NotFound();
            }

            return Ok(nutritionPlans);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNutritionPlan([FromBody] NutritionPlanDto nutritionPlanDto)
        {
            if (nutritionPlanDto == null)
                return BadRequest(ModelState);

            var nutritionPlan = _nutritionPlanRepository.GetAllNutritionPlans()
                .Where(c => c.Name.Trim().ToUpper() == nutritionPlanDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (nutritionPlan != null)
            {
                ModelState.AddModelError("", "Nutrition plan already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nutritionPlanMap = _mapper.Map<NutritionPlan>(nutritionPlanDto);

            if (!_nutritionPlanRepository.CreateNutritionPlan(nutritionPlanMap))
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
        public IActionResult UpdateNutritionPlan(int id, [FromBody] NutritionPlanDto nutritionPlan)
        {
            if (nutritionPlan == null)
                return BadRequest(ModelState);

            if (id != nutritionPlan.Id)
                return BadRequest(ModelState);

            if (!_nutritionPlanRepository.NutritionPlanExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var nutritionPlanMap = _mapper.Map<NutritionPlan>(nutritionPlan);

            if (!_nutritionPlanRepository.UpdateNutritionPlan(nutritionPlanMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}