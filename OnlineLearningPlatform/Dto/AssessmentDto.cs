using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Dto
{
    public class AssessmentDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int StudentAssignmentId { get; set; }
    }
}
