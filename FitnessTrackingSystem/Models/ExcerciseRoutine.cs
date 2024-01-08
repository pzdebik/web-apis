namespace FitnessTrackingSystem.Models
{
    public class ExcerciseRoutine
    {
        public int RoutineId { get; set; }
        public Routine Routine { get; set; }
        public int ExcerciseId { get; set; }
        public Excercise Excercise { get; set; }
    }
}
