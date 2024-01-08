using FitnessTrackingSystem.Data;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Repository
{
    public class RoutineRepository : IRoutineRepository
    {
        private readonly DataContext _context;
        public RoutineRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Routine> GetAllRoutines()
        {
            return _context.Routines.ToList();
        }

        public ICollection<Routine> GetAllRoutinesByUserId(int userId)
        {
            // nie jestem pewien wyniku końcowego
            return _context.Workouts.Where(r => r.UserId == userId).Select(r => r.Routine).ToList();
        }

        public Routine GetRoutine(int id)
        {
            return _context.Routines.Where(r => r.Id == id).FirstOrDefault();
        }

        public bool RoutineExists(int routineId)
        {
            return _context.Routines.Any(e => e.Id == routineId);
        }

        public bool CreateRoutine(Routine routine)
        {
            _context.Routines.Add(routine);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UpdateRoutine(Routine routine)
        {
            _context.Routines.Update(routine);
            return Save();
        }
    }
}
