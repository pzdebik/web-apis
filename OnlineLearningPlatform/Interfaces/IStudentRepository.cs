using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<User> GetAllStudents();
        User GetStudent(int id);
        decimal GetStudentAvgScore(int studId);
        ICollection<Assessment> GetStudentAssessments(int studId);
        bool StudentExists(int studId);
    }
}
