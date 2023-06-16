using TestProject.Domain.Commons;
using TestProject.Domain.Enums;

namespace TestProject.Domain.Entities
{
    public class Student : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public long StudentRegNumber { get; set; }
        public UserRole Role { get; set; }
        public List<StudentSubject> StudentSubjects { get; set; }
    }
}
