using AutoMapper;
using OnlineLearningPlatform.Dto;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, StudentDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Assignment, AssignmentDto>();
            CreateMap<Assessment, AssessmentDto>();
            CreateMap<Enrolment, EnrolmentDto>();
            CreateMap<EnrolmentDto, Enrolment>();
            CreateMap<StudentAssignment, StudentAssignmentDto>();
            CreateMap<StudentAssignmentDto, StudentAssignment>();
        }
    }
}
