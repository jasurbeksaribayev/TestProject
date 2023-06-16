using TestProject.Service.DTOs.Student;
using TestProject.Service.DTOs.Subject;

namespace TestProject.Service.DTOs.StudentSubject
{
    public class StudentSubjectForCreationDTO
    {
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}
