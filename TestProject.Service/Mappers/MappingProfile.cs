using AutoMapper;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Student;
using TestProject.Service.DTOs.StudentSubject;
using TestProject.Service.DTOs.Subject;
using TestProject.Service.DTOs.Teacher;

namespace TestProject.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Student 
            CreateMap<Student, StudentForCreationDTO>().ReverseMap();
            CreateMap<Student, StudentForViewDTO>().ReverseMap();

            // Teacher
            CreateMap<Teacher, TeacherForCreationDTO>().ReverseMap();
            CreateMap<Teacher, TeacherForViewDTO>().ReverseMap();

            // Subject
            CreateMap<Subject, SubjectForCreationDTO>().ReverseMap();
            CreateMap<Subject, SubjectForViewDTO>().ReverseMap();

            // StudentSubject
            CreateMap<StudentSubject, StudentSubjectForCreationDTO>().ReverseMap();
            CreateMap<StudentSubject, StudentSubjectForViewDTO>().ReverseMap();
        }
    }
}
