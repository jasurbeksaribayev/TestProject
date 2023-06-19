using System.Linq.Expressions;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Students;

namespace TestProject.Service.IServices.Students
{
    public interface IStudentService
    {
        Task<StudentForViewDTO> CreateAsync(StudentForCreationDTO studentForCreationDTO);
        Task<StudentForViewDTO> UpdateAsync(int id, StudentForCreationDTO studentForCreationDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StudentForViewDTO>> GetByAgeAsync();
        Task<IEnumerable<StudentForViewDTO>> GetDateAsync();
        Task<IEnumerable<StudentForViewDTO>> GetSearchAsync(Expression<Func<Student, bool>> func);
        Task<StudentForViewDTO> GetMaxScoreSubjectAsync(int id);
        Task<IEnumerable<StudentForViewDTO>> GetMobileNumberStartWith90And91Async();
        Task<IEnumerable<StudentForViewDTO>> GetAllAsync();
    }
}