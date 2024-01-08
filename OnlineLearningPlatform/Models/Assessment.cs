namespace OnlineLearningPlatform.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public int StudentAssignmentId { get; set; }
        public StudentAssignment StudentAssignment { get; set; }
    }
}
