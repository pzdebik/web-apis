using FitnessTrackingSystem.Data;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly DataContext _context;
        public WorkoutRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Workout> GetAllWorkouts()
        {
            return _context.Workouts.ToList();
        }

        public ICollection<Workout> GetAllWorkoutsByUserId(int userId)
        {
            return _context.Workouts.Where(w => w.UserId == userId).ToList();
        }

        public Workout GetWorkout(int id)
        {
            return _context.Workouts.Where(w => w.Id == id).FirstOrDefault();
        }

        public bool WorkoutExists(int workoutId)
        {
            return _context.Workouts.Any(e => e.Id == workoutId);
        }

        public bool CreateWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UpdateWorkout(Workout workout)
        {
            _context.Workouts.Update(workout);
            return Save();
        }
    }
}
