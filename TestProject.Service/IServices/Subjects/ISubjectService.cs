using TestProject.Service.DTOs.Subjects;

namespace TestProject.Service.IServices.Subjects
{
    public interface ISubjectService
    {
        Task<SubjectForViewDTO> CreateAsync(SubjectForCreationDTO subjectForCreationDTO);
        Task<SubjectForViewDTO> UpdateAsync(int id, SubjectForCreationDTO subjectForCreationDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SubjectForViewDTO>> GetAllAsync();
    }
}
