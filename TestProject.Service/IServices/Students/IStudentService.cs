using System.Linq.Expressions;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Student;

namespace TestProject.Service.IServices.Students
{
    public interface IStudentService
    {
        Task<StudentForViewDTO> CreateAsync(StudentForCreationDTO studentForCreationDTO);
        Task<StudentForViewDTO> UpdatesAsync(int id, StudentForCreationDTO studentForCreationDTO);
        Task<bool> DeletesAsync(int id);
        Task<IEnumerable<StudentForViewDTO>> GetAgeAsync();
        Task<IEnumerable<StudentForViewDTO>> GetDateAsync();
        Task<StudentForViewDTO> GetSearchAsync(Expression<Func<Student, bool>> func);
        Task<StudentForViewDTO> GetMaxScoreSubjectAsync(int id);
        Task<StudentForViewDTO> GetMaxScoreTeacherAsync();
        Task<StudentForViewDTO> GetAvarageScoreThemeAsync();
        Task<IEnumerable<StudentForViewDTO>> GetAllAsync();
    }
}