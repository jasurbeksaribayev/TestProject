using TestProject.Domain.Enums;
using TestProject.Service.DTOs.Subject;

namespace TestProject.Service.DTOs.Teacher
{
    public class TeacherForViewDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }
        public ICollection<SubjectForViewDTO> Subjects { get; set; }
    }
}
