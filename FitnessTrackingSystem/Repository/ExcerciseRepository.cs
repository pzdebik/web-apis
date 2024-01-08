using FitnessTrackingSystem.Data;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Repository
{
    public class ExcerciseRepository : IExcerciseRepository
    {
        private readonly DataContext _context;
        public ExcerciseRepository(DataContext context)
        {
            _context = context;
        }
        public bool ExcerciseExists(int excerciseId)
        {
            return _context.Excercises.Any(e => e.Id == excerciseId);
        }

        public ICollection<Excercise> GetAllExcercises()
        {
            return _context.Excercises.ToList();
        }

        public ICollection<ExcerciseRoutine> GetAllExcercisesByRoutineId(int routineId)
        {
            return _context.ExcerciseRoutines.Where(e => e.RoutineId == routineId).ToList();
        }

        public Excercise GetExcercise(int id)
        {
            return _context.Excercises.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool CreateExcercise(Excercise excercise)
        {
            _context.Excercises.Add(excercise);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UpdateExcercise(Excercise excercise)
        {
            _context.Excercises.Update(excercise);
            return Save();
        }
    }
}
