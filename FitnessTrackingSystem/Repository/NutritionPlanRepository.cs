using FitnessTrackingSystem.Data;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Repository
{
    public class NutritionPlanRepository : INutritionPlanRepository
    {
        private readonly DataContext _context;
        public NutritionPlanRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<NutritionPlan> GetAllNutritionPlans()
        {
            return _context.NutritionPlans.ToList();
        }

        public ICollection<NutritionPlan> GetAllNutritionPlansByUserId(int userId)
        {
            return _context.NutritionPlans.Where(n => n.UserId == userId).ToList();
        }

        public NutritionPlan GetNutritionPlan(int id)
        {
            return _context.NutritionPlans.Where(n => n.Id == id).FirstOrDefault();
        }

        public bool NutritionPlanExists(int nutritionPlanId)
        {
            return _context.NutritionPlans.Any(e => e.Id == nutritionPlanId);
        }

        public bool CreateNutritionPlan(NutritionPlan nutritionPlan)
        {
            _context.NutritionPlans.Add(nutritionPlan);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UpdateNutritionPlan(NutritionPlan nutritionPlan)
        {
            _context.NutritionPlans.Update(nutritionPlan);
            return Save();
        }
    }
}
