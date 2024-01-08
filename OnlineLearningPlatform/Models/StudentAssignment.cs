namespace OnlineLearningPlatform.Models
{
    public class StudentAssignment
    {
        public int Id { get; set; }
        public Status Status { get; set; } = Status.IN_PROGRESS;
        public int UserId { get; set; }
        public User User { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public Assessment Assessment { get; set; }
    }
}
public enum Status
{
    IN_PROGRESS,
    COMPLETED,
    NOT_COMPLETED
}