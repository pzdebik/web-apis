using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly DataContext _context;
        public AssignmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool AssignmentExists(int assignmentId)
        {
            return _context.Assignments.Any(e => e.Id == assignmentId);
        }

        public ICollection<Assignment> GetAllAssignments()
        {
            return _context.Assignments.ToList();
        }

        public Assignment GetAssignment(int id)
        {
            return _context.Assignments.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Assessment> GetAssessmentsFromAssignment(int assignmentId)
        {
            return _context.Assessments.Where(e => e.StudentAssignment.AssignmentId == assignmentId).ToList();
        }
    }
}
