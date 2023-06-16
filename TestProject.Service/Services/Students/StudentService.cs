using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Domain.Enums;
using TestProject.Service.DTOs.Student;
using TestProject.Service.Exceptions;
using TestProject.Service.IServices.Students;

namespace TestProject.Service.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> studentRepository;
        private readonly IMapper mapper;

        public StudentService(IGenericRepository<Student> studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        public async Task<StudentForViewDTO> CreateAsync(StudentForCreationDTO studentForCreationDTO)
        {
            var existStudent = await studentRepository.GetAsync(s => s.Email.Equals(studentForCreationDTO.Email));

            if (existStudent is not null)
                throw new TestProjectException(404, "this email already exist");

            studentForCreationDTO.StudentRegNumber = studentRepository.GetAll().Max(s => s.Id) + 1;
            var student = await studentRepository.CreateAsync(mapper.Map<Student>(studentForCreationDTO));
            await studentRepository.SaveChangesAsync();

            return mapper.Map<StudentForViewDTO>(student);
        }

        public async Task<bool> DeletesAsync(int id)
        {
            var existStudent = await studentRepository.DeleteAsync(id);

            if (!existStudent)
                throw new TestProjectException(404, "not found");
            
            await studentRepository.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<StudentForViewDTO>> GetAgeAsync()
        {
            var existStudentAgeToTwenty = studentRepository.GetAll(s => DateTime.Now.Year - s.BirthDate.Year <= 20);

            if (existStudentAgeToTwenty is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IQueryable<StudentForViewDTO>>(existStudentAgeToTwenty);
        }
        public async Task<IEnumerable<StudentForViewDTO>> GetDateAsync()
        {
            var existStudentsBornBetweenAugust18AndSeptember20 = studentRepository.GetAll(s => s.BirthDate.Month == 8 && s.BirthDate.Day >= 12 || s.BirthDate.Month == 9 && s.BirthDate.Day <= 18);

            if (existStudentsBornBetweenAugust18AndSeptember20 is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IQueryable<StudentForViewDTO>>(existStudentsBornBetweenAugust18AndSeptember20);
        }
        public Task<StudentForViewDTO> GetSearchAsync(Expression<Func<Student, bool>> func);
        public Task<StudentForViewDTO> GetMaxScoreSubjectAsync(int id);
        public Task<StudentForViewDTO> GetMaxScoreTeacherAsync();
        public Task<StudentForViewDTO> GetAvarageScoreThemeAsync();
        public Task<IEnumerable<StudentForViewDTO>> GetAllAsync();

        public async Task<StudentForViewDTO> UpdatesAsync(int id, StudentForCreationDTO studentForCreationDTO)
        {
            var existStudent = await studentRepository.GetAsync(s => s.Id.Equals(id));

            if (existStudent == null)
                throw new TestProjectException(404, "not found");

            var alreadyExistStudent = await studentRepository.GetAsync(
                u => u.Email == studentForCreationDTO.Email && u.Id != id);

            if (alreadyExistStudent != null)
                throw new TestProjectException(400, "this email already exists");

            existStudent.LastModifiedTime = DateTime.UtcNow;
            existStudent = studentRepository.Update(mapper.Map(studentForCreationDTO, existStudent));

            await studentRepository.SaveChangesAsync();

            return mapper.Map<StudentForViewDTO>(existStudent);
        }
    }
}
