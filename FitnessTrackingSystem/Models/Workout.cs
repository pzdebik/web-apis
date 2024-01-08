namespace FitnessTrackingSystem.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; } 
        public long CaloriesBurned { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int RoutineId { get; set; }
        public Routine Routine { get; set; }
    }
}
