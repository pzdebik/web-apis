namespace OnlineLearningPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; }
        


    }
}
