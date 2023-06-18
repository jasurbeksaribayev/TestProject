using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Service.DTOs.Subject;
using TestProject.Service.DTOs.Teacher;

namespace TestProject.Service.IServices.Subjects
{
    public interface ISubjectService
    {
        Task<SubjectForViewDTO> CreateAsync(SubjectForCreationDTO subjectForCreationDTO);
        Task<SubjectForViewDTO> UpdatesAsync(int id, SubjectForCreationDTO subjectForCreationDTO);
        Task<bool> DeletesAsync(int id);
        Task<IEnumerable<SubjectForViewDTO>> GetAllAsync();
    }
}
