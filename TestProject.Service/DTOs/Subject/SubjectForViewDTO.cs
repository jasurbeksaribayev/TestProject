using TestProject.Service.DTOs.StudentSubject;

namespace TestProject.Service.DTOs.Subject
{
    public class SubjectForViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public List<StudentSubjectForViewDTO> StudentSubjects { get; set; }
    }
}
