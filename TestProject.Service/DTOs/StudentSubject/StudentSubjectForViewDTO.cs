using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Students;
using TestProject.Service.DTOs.Subjects;

namespace TestProject.Service.DTOs.StudentSubject
{
    public class StudentSubjectForViewDTO
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public StudentForViewDTO Student { get; set; }
        public int SubjectId { get; set; }
        public SubjectForViewDTO Subject { get; set; }
    }
}
