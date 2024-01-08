using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IStudentAssignmentRepository
    {
        ICollection<StudentAssignment> GetAllStudentAssignments();
        StudentAssignment GetStudentAssignment(int id);
        ICollection<StudentAssignment> GetStudentAssignmentsByStudentId(int studId);
        bool StudentAssignmentExists(int studId);
        bool UpdateStudentAssignment(StudentAssignment studentAssignment);
        bool Save();
    }
}
