using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Interfaces
{
    public interface IRoutineRepository
    {
        ICollection<Routine> GetAllRoutines();
        Routine GetRoutine(int id);
        ICollection<Routine> GetAllRoutinesByUserId(int userId);
        bool RoutineExists(int routineId);
        bool CreateRoutine(Routine routine);
        bool UpdateRoutine(Routine routine);
        bool Save();
    }
}
