using AutoMapper;
using FitnessTrackingSystem.Dto;
using FitnessTrackingSystem.Models;

namespace FitnessTrackingSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Excercise, ExcerciseDto>();
            CreateMap<ExcerciseDto, Excercise>();
            CreateMap<NutritionPlan, NutritionPlanDto>();
            CreateMap<NutritionPlanDto, NutritionPlan>();
            CreateMap<ProgressTracking, ProgressTrackingDto>();
            CreateMap<ProgressTrackingDto, ProgressTracking>();
            CreateMap<Routine, RoutineDto>();
            CreateMap<RoutineDto, Routine>();
            CreateMap<Workout, WorkoutDto>();
            CreateMap<WorkoutDto, Workout>();
            CreateMap<ExcerciseRoutine, ExcerciseRoutineDto>();
            CreateMap<ExcerciseRoutineDto, ExcerciseRoutine>();
        }
    }
}
