using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Repository
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly DataContext _context;

        public AssessmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool AssessmentExists(int assessmentId)
        {
            return _context.Assessments.Any(a => a.Id == assessmentId);
        }

        public ICollection<Assessment> GetAllAssessments()
        {
            return _context.Assessments.ToList();
        }

        public Assessment GetAssessment(int id)
        {
            return _context.Assessments.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Assessment> GetAssessmentsByStudent(int studId)
        {
            return _context.Assessments.Where(a => a.StudentAssignment.UserId == studId).ToList();
        }
    }
}
