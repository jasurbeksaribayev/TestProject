using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Service.DTOs.Student;
using TestProject.Service.DTOs.Subject;

namespace TestProject.Service.DTOs.StudentSubject
{
    public class StudentSubjectForViewDTO
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public StudentForViewDTO Student { get; set; }
        public int SubjectId { get; set; }
        public SubjectForViewDTO Subject { get; set; }
    }
}
