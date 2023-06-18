using TestProject.Domain.Enums;

namespace TestProject.Service.DTOs.Teacher
{
    public class TeacherForCreationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }
    }
}
