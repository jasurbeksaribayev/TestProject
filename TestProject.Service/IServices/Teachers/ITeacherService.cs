using AutoMapper;
using TestProject.Service.DTOs.Students;
using TestProject.Service.DTOs.Teacher;
using TestProject.Service.Exceptions;

namespace TestProject.Service.IServices.Teachers
{
    public interface ITeacherService
    {
        Task<TeacherForViewDTO> CreateAsync(TeacherForCreationDTO teacherForCreationDTO);
        Task<TeacherForViewDTO> UpdateAsync(int id, TeacherForCreationDTO teacherForCreationDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TeacherForViewDTO>> GetByAgeAsync();
        Task<IEnumerable<TeacherForViewDTO>> GetMobileNumberStartWith90And91Async();
        Task<IEnumerable<TeacherForViewDTO>> GetAllAsync();
    }
}
