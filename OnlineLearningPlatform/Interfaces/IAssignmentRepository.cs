using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IAssignmentRepository
    {
        ICollection<Assignment> GetAllAssignments();
        Assignment GetAssignment(int id);
        ICollection<Assessment> GetAssessmentsFromAssignment(int assignmentId);
        bool AssignmentExists(int assignmentId);
    }
}
