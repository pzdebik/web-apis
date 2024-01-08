using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface ICourseRepository
    {
        ICollection<Course> GetAllCourses();
        Course GetCourse(int id);
        ICollection<Assignment> GetAssignmentsFromCourse(int courseId);
        bool CourseExists(int id);
    }
}
