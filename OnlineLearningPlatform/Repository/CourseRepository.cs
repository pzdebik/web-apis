using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public bool CourseExists(int id)
        {
            return _context.Courses.Any(c => c.Id == id);
        }

        public ICollection<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourse(int id)
        {
            return _context.Courses.Where(e => e.Id == id).FirstOrDefault();
        }
        public ICollection<Assignment> GetAssignmentsFromCourse(int courseId)
        {
            return _context.Assignments.Where(c => c.CourseId == courseId).ToList();
        }
    }
}
