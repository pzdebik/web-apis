namespace OnlineLearningPlatform.Dto
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
    }
}
