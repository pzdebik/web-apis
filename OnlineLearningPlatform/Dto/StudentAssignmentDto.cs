using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Dto
{
    public class StudentAssignmentDto
    {
        public int Id { get; set; }
        public Status Status { get; set; } = Status.IN_PROGRESS;
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
    }
}
