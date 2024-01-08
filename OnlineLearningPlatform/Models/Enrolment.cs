namespace OnlineLearningPlatform.Models
{
    public class Enrolment
    {
        public int CourseId { get; set; }
        public Course Course { get; set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime EnrolmentDate { get; set; } = DateTime.Now;
    }
}
