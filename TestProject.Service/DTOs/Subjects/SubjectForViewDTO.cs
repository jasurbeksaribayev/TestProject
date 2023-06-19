using TestProject.Service.DTOs.Students;
using TestProject.Service.DTOs.StudentSubject;
using TestProject.Service.DTOs.Teacher;

namespace TestProject.Service.DTOs.Subjects
{
    public class SubjectForViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public TeacherForViewDTO Teacher { get; set; }
        public List<StudentSubjectForViewDTO> StudentSubjects { get; set; }
    }
}
