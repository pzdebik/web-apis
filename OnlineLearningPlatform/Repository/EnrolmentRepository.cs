using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Repository
{
    public class EnrolmentRepository : IEnrolmentRepository
    {
        private readonly DataContext _context;
        public EnrolmentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateEnrolment(Enrolment enrolment)
        {
            _context.Add(enrolment);
            return Save();
        }

        public bool EnrolmentExists(int id)
        {

            return _context.Enrolments.Any(e => e.UserId == id);
        }

        public ICollection<Enrolment> GetAllEnrolments()
        {
            return _context.Enrolments.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
