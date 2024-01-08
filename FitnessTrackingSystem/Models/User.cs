namespace FitnessTrackingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? PasswordHash { get; set; }

        public ICollection<ProgressTracking> ProgressTrackings { get; set; }
        public ICollection<NutritionPlan> NutritionPlans { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
    
}
public enum Gender
{
    Male,
    Female
}
