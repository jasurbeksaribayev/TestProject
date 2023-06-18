using TestProject.Domain.Commons;

namespace TestProject.Domain.Entities
{
    public class Subject : Auditable
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<StudentSubject> StudentSubjects { get; set; }
    }
}
