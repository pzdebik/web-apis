using FitnessTrackingSystem.Data;
using FitnessTrackingSystem.Interfaces;
using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Repository
{
    public class ProgressTrackingRepository : IProgressTrackingRepository
    {
        private readonly DataContext _context;
        public ProgressTrackingRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<ProgressTracking> GetAllProgressTrackings()
        {
            return _context.ProgressTrackings.ToList();
        }

        public ICollection<ProgressTracking> GetAllProgressTrackingsByUserId(int userId)
        {
            return _context.ProgressTrackings.Where(p => p.UserId == userId).ToList();
        }

        public ProgressTracking GetProgressTracking(int id)
        {
            return _context.ProgressTrackings.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool ProgressTrackingExists(int progressTrackingId)
        {
            return _context.ProgressTrackings.Any(e => e.Id == progressTrackingId);
        }

        public bool CreateProgressTracking(ProgressTracking progressTracking)
        {
            _context.ProgressTrackings.Add(progressTracking);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UpdateProgressTracking(ProgressTracking progressTracking)
        {
            _context.ProgressTrackings.Update(progressTracking);
            return Save();
        }
    }
}
