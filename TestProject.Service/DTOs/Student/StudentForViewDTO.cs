using TestProject.Domain.Enums;
using TestProject.Service.DTOs.StudentSubject;

namespace TestProject.Service.DTOs.Student
{
    public class StudentForViewDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public long StudentRegNumber { get; set; }
        public UserRole Role { get; set; }
        public List<StudentSubjectForViewDTO> StudentSubjects { get; set; }
    }
}
