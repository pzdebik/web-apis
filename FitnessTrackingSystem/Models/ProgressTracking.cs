namespace FitnessTrackingSystem.Models
{
    public class ProgressTracking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int BodyFatPercentage { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
