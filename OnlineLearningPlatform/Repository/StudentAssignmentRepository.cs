using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Repository
{
    public class StudentAssignmentRepository : IStudentAssignmentRepository
    {
        private readonly DataContext _context;
        public StudentAssignmentRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<StudentAssignment> GetAllStudentAssignments()
        {
            return _context.StudentAssignments.ToList();
        }

        public StudentAssignment GetStudentAssignment(int id)
        {
            return _context.StudentAssignments.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<StudentAssignment> GetStudentAssignmentsByStudentId(int studId)
        {
            return _context.StudentAssignments.Where(s => s.User.Id == studId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StudentAssignmentExists(int studId)
        {
            return _context.StudentAssignments.Any(s => s.Id == studId);
        }

        public bool UpdateStudentAssignment(StudentAssignment studentAssignment)
        {
            _context.Update(studentAssignment);
            return Save();
        }
    }
}
