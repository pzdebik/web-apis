namespace OnlineLearningPlatform.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set;}
        public ICollection<Assessment> Assessments { get; set; }
        public ICollection<StudentAssignment> StudentAssignments { get; set; }
    }
}
