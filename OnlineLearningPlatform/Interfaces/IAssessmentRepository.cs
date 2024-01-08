using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IAssessmentRepository
    {
        ICollection<Assessment> GetAllAssessments();
        Assessment GetAssessment(int id);
        ICollection<Assessment> GetAssessmentsByStudent (int studId);
        bool AssessmentExists(int assessmentId);
    }
}
