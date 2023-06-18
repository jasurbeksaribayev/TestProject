using TestProject.Domain.Commons;
using TestProject.Domain.Enums;

namespace TestProject.Domain.Entities
{
    public class Teacher : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
