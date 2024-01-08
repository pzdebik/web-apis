using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Interfaces
{
    public interface IWorkoutRepository
    {
        ICollection<Workout> GetAllWorkouts();
        Workout GetWorkout(int id);
        ICollection<Workout> GetAllWorkoutsByUserId(int userId);
        bool WorkoutExists(int workoutId);
        bool CreateWorkout(Workout workout);
        bool UpdateWorkout(Workout workout);
        bool Save();
    }
}
