using TestProject.Service.DTOs.Students;
using TestProject.Service.DTOs.StudentSubject;

namespace TestProject.Service.IServices.StudentSubjects
{
    public interface IStudentSubjectService
    {
        Task<StudentSubjectForViewDTO> CreateAsync(StudentSubjectForCreationDTO studentSubjectForCreationDTO);
        Task<StudentSubjectForViewDTO> UpdateAsync(int id, StudentSubjectForCreationDTO studentSubjectForCreationDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StudentSubjectForViewDTO>> GetMaxScoreTeacherAsync();
        Task<IEnumerable<StudentSubjectForViewDTO>> GetAllAsync();
    }
}
