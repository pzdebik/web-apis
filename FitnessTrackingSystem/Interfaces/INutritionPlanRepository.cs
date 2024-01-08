using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Interfaces
{
    public interface INutritionPlanRepository
    {
        ICollection<NutritionPlan> GetAllNutritionPlans();
        NutritionPlan GetNutritionPlan(int id);
        ICollection<NutritionPlan> GetAllNutritionPlansByUserId(int userId);
        bool NutritionPlanExists(int nutritionPlanId);
        bool CreateNutritionPlan(NutritionPlan nutritionPlan);
        bool UpdateNutritionPlan(NutritionPlan nutritionPlan);
        bool Save();
    }
}
