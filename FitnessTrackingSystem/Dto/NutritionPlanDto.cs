namespace FitnessTrackingSystem.Dto
{
    public class NutritionPlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CaloriesGoal { get; set; }
        public int UserId { get; set; }
    }
}
