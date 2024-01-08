using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Interfaces
{
    public interface IEnrolmentRepository
    {
        ICollection<Enrolment> GetAllEnrolments();
        bool EnrolmentExists (int id);
        bool CreateEnrolment (Enrolment enrolment);
        bool Save();
    }
}
