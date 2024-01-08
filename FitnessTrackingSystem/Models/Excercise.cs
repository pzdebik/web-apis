namespace FitnessTrackingSystem.Models
{
    public class Excercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string EquipmentRequired { get; set; }

        public ICollection<ExcerciseRoutine> ExcerciseRoutines { get; set; }
    }
}
