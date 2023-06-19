using System.ComponentModel.DataAnnotations;
using TestProject.Domain.Enums;

namespace TestProject.Service.DTOs.Students
{
    public class StudentForCreationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^\+9989\d{8}$",
        ErrorMessage = "Phone number must start with +9989 and consist of 7 digits")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int StudentRegNumber { get; set; }
        public UserRole Role { get; set; }
    }
}
