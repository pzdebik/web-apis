namespace FitnessTrackingSystem.Dto
{
    public class ProgressTrackingDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int BodyFatPercentage { get; set; }
        public int UserId { get; set; }
    }
}
