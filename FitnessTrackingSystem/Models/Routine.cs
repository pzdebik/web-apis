namespace FitnessTrackingSystem.Models
{
    public class Routine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Workout> Workouts { get; set; }
        public ICollection<ExcerciseRoutine> ExcerciseRoutines { get; set; }
    }
}
