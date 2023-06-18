using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Student;
using TestProject.Service.DTOs.Teacher;

namespace TestProject.Service.IServices.Teachers
{
    public interface ITeacherService
    {
        Task<TeacherForViewDTO> CreateAsync(TeacherForCreationDTO teacherForCreationDTO);
        Task<TeacherForViewDTO> UpdatesAsync(int id, TeacherForCreationDTO teacherForCreationDTO);
        Task<bool> DeletesAsync(int id);
        Task<IEnumerable<TeacherForViewDTO>> GetAgeAsync();
        Task<IEnumerable<TeacherForViewDTO>> GetAllAsync();
    }
}
