using Microsoft.Win32;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Interfaces;
using OnlineLearningPlatform.Models;

// w Repository dodajemy wyłącznie odwołania do bazy danych
// jeśli w Repository chcemy dodać coś więcej niż odwołania to nazywać się to będzie Services
namespace OnlineLearningPlatform.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetAllStudents()
        {
            // kiedy zwracasz coś z bazy danych musisz być szczegółowy w jaki sposób chcesz to zwrócic
            // nie wystarczy 'return _context.Students', dodaj ToList()
            return _context.Users.OrderBy(x => x.Id).ToList();
        }

        public User GetStudent(int id)
        {
            return _context.Users.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<Assessment> GetStudentAssessments(int studId)
        {
            return _context.Assessments.Where(a => a.StudentAssignment.UserId == studId).ToList();
        }

        // zwraca średni wynik uzyskany w kursach
        public decimal GetStudentAvgScore(int studId)
        {
            var score = _context.Assessments.Where(s => s.StudentAssignment.UserId == studId);

            if (score.Count() <= 0)
                return 0;

            return ((decimal)score.Sum(s => s.Score) / score.Count());
        }

        public bool StudentExists(int studId)
        {
            return _context.Users.Any(s => s.Id == studId);
        }
    }
}
