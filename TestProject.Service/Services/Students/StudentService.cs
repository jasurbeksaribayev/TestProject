using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Students;
using TestProject.Service.Exceptions;
using TestProject.Service.IServices.Students;

namespace TestProject.Service.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> studentRepository;
        private readonly IGenericRepository<Teacher> teacherRepository;
        private readonly IGenericRepository<StudentSubject> studentSubjectRepository;
        private readonly IMapper mapper;

        public StudentService(IGenericRepository<Student> studentRepository, IGenericRepository<Teacher> teacherRepository, IGenericRepository<StudentSubject> studentSubjectRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
            this.studentSubjectRepository = studentSubjectRepository;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var existStudent = await studentRepository.DeleteAsync(id);

            if (!existStudent)
                throw new TestProjectException(404, "not found");

            await studentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<StudentForViewDTO>> GetByAgeAsync()
        {
            var existStudentAgeToTwenty = studentRepository.GetAll(s => (DateTime.Now.Year - s.BirthDate.Year) <= 20).
                Include(s => s.StudentSubjects).
                ThenInclude(s => s.Subject).
                ThenInclude(t => t.Teacher);

            if (existStudentAgeToTwenty is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentForViewDTO>>(existStudentAgeToTwenty);
        }
        public async Task<IEnumerable<StudentForViewDTO>> GetDateAsync()
        {
            var existStudentsBornBetweenAugust18AndSeptember20 = studentRepository.
                GetAll(s => s.BirthDate.Month == 8 && s.BirthDate.Day >= 12 || s.BirthDate.Month == 9 && s.BirthDate.Day <= 18);

            if (existStudentsBornBetweenAugust18AndSeptember20 is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentForViewDTO>>(existStudentsBornBetweenAugust18AndSeptember20);
        }
        public async Task<IEnumerable<StudentForViewDTO>> GetSearchAsync(Expression<Func<Student, bool>> func)
        {
            var existStudents = studentRepository.GetAll(func);

            if (existStudents is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentForViewDTO>>(existStudents);
        }
        public async Task<StudentForViewDTO> GetMaxScoreSubjectAsync(int id)
        {
            var maxScore = studentSubjectRepository.GetAll(s => s.StudentId == id).Max(u => u.Grade);

            var existStudent = studentRepository.GetAll(s => s.Id == id).
                Include(ss=>ss.StudentSubjects.Where(ss=>ss.Grade == maxScore)).ThenInclude(s=>s.Subject).FirstOrDefault();

            if (existStudent is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<StudentForViewDTO>(existStudent);
        }
        
        public async Task<IEnumerable<StudentForViewDTO>> GetMobileNumberStartWith90And91Async()
        {
            var students = studentRepository.GetAll(s => s.PhoneNumber.StartsWith("+99890") || s.PhoneNumber.StartsWith("+99891"));
            
            if (students is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentForViewDTO>>(students);    
        }
        public async Task<IEnumerable<StudentForViewDTO>> GetAllAsync()
        {
            var existStudent = studentRepository.GetAll().
                Include(s => s.StudentSubjects).
                ThenInclude(s => s.Subject).
                ThenInclude(t => t.Teacher);

            if (existStudent is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentForViewDTO>>(existStudent);
        }

        public async Task<StudentForViewDTO> UpdateAsync(int id, StudentForCreationDTO studentForCreationDTO)
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
