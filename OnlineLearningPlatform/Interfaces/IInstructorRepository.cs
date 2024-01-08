using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IInstructorRepository
    {
        ICollection<Instructor> GetAllInstructors();
        Instructor GetInstructor(int id);
        Instructor GetInstructor(string name);
        bool InstructorExists(int instructorId);
    }
}
