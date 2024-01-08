using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Dto
{
    public class EnrolmentDto
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public DateTime EnrolmentDate { get; set; }
    }
}
