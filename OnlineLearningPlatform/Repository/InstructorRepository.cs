using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly DataContext _context;
        public InstructorRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Instructor> GetAllInstructors()
        {
            return _context.Instructors.ToList();
        }

        public Instructor GetInstructor(int id)
        {
            return _context.Instructors.Where(e => e.Id == id).FirstOrDefault();
        }

        public Instructor GetInstructor(string name)
        {
            return _context.Instructors.Where(e => e.Name == name).FirstOrDefault();
        }

        public bool InstructorExists(int instructorId)
        {
            return _context.Instructors.Any(e => e.Id == instructorId);
        }
    }
}
