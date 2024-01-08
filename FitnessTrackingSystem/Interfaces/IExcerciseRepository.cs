using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Interfaces
{
    public interface IExcerciseRepository
    {
        ICollection<Excercise> GetAllExcercises();
        Excercise GetExcercise(int id);
        ICollection<ExcerciseRoutine> GetAllExcercisesByRoutineId(int routineId);
        bool ExcerciseExists(int excerciseId);
        bool CreateExcercise(Excercise excercise);
        bool UpdateExcercise(Excercise excercise);
        bool Save();
    }
}
