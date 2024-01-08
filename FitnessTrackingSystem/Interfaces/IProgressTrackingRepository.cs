using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Interfaces
{
    public interface IProgressTrackingRepository
    {
        ICollection<ProgressTracking> GetAllProgressTrackings();
        ProgressTracking GetProgressTracking(int id);
        ICollection<ProgressTracking> GetAllProgressTrackingsByUserId(int userId);
        bool ProgressTrackingExists(int progressTrackingId);
        bool CreateProgressTracking(ProgressTracking progressTracking);
        bool UpdateProgressTracking(ProgressTracking progressTracking);
        bool Save();
    }
}
