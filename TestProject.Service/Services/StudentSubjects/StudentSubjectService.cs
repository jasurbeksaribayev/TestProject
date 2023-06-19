using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.StudentSubject;
using TestProject.Service.Exceptions;
using TestProject.Service.IServices.StudentSubjects;

namespace TestProject.Service.Services.StudentSubjects
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly IGenericRepository<StudentSubject> studentSubjectRepository;
        private readonly IGenericRepository<Student> studentRepository;
        private readonly IGenericRepository<Subject> subjectRepository;
        private readonly IMapper mapper;

        public StudentSubjectService(IGenericRepository<StudentSubject> studentSubjectRepository, IGenericRepository<Student> studentRepository, IGenericRepository<Subject> subjectRepository, IMapper mapper)
        {
            this.studentSubjectRepository = studentSubjectRepository;
            this.studentRepository = studentRepository;
            this.subjectRepository = subjectRepository;
            this.mapper = mapper;
        }

        public async Task<StudentSubjectForViewDTO> CreateAsync(StudentSubjectForCreationDTO studentSubjectForCreationDTO)
        {
            var existStudent = await studentRepository.GetAsync(s => s.Id.Equals(studentSubjectForCreationDTO.StudentId));

            if (existStudent is null)
                throw new TestProjectException(404, "student not found");

            var existSubject = await subjectRepository.GetAsync(s => s.Id.Equals(studentSubjectForCreationDTO.SubjectId));

            if (existSubject is null)
                throw new TestProjectException(404, "subject not found");

            var existStudentSubject = await studentSubjectRepository.GetAsync(s => s.StudentId.Equals(studentSubjectForCreationDTO.StudentId) && s.SubjectId.Equals(studentSubjectForCreationDTO.SubjectId));

            if (existStudentSubject is not null)
                throw new TestProjectException(404, "student with this subject already exist");

            var studentSubject = await studentSubjectRepository.CreateAsync(mapper.Map<StudentSubject>(studentSubjectForCreationDTO));
            await studentSubjectRepository.SaveChangesAsync();

            return mapper.Map<StudentSubjectForViewDTO>(studentSubject);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existStudentSubject = await studentSubjectRepository.DeleteAsync(id);

            if (!existStudentSubject)
                throw new TestProjectException(404, "not found");

            await studentSubjectRepository.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<StudentSubjectForViewDTO>> GetMaxScoreTeacherAsync()
        {
            var existStudentSubject = studentSubjectRepository.GetAll(s => s.Grade >= 97).
                Include(ss => ss.Student).
                Include(ss => ss.Subject).
                ThenInclude(s => s.Teacher);

            if (existStudentSubject is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentSubjectForViewDTO>>(existStudentSubject);
        }

        public async Task<IEnumerable<StudentSubjectForViewDTO>> GetAllAsync()
        {
            var existStudentSubject = studentSubjectRepository.GetAll().
                Include(ss=>ss.Student).
                Include(ss=>ss.Subject).
                ThenInclude(s=>s.Teacher);

            if (existStudentSubject is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<StudentSubjectForViewDTO>>(existStudentSubject);
        }

        public async Task<StudentSubjectForViewDTO> UpdateAsync(int id, StudentSubjectForCreationDTO studentSubjectForCreationDTO)
        {
            var existStudent = await studentRepository.GetAsync(s => s.Id.Equals(studentSubjectForCreationDTO.StudentId));

            if (existStudent is null)
                throw new TestProjectException(404, "student not found");

            var existSubject = await subjectRepository.GetAsync(s => s.Id.Equals(studentSubjectForCreationDTO.SubjectId));

            if (existSubject is null)
                throw new TestProjectException(404, "subject not found");

            var existStudentSubject = await studentSubjectRepository.GetAsync(s => s.StudentId.Equals(studentSubjectForCreationDTO.StudentId) && s.SubjectId.Equals(studentSubjectForCreationDTO.SubjectId) && s.Id != id);

            if (existStudentSubject is not null)
                throw new TestProjectException(404, "student with this subject already exist");

            existStudentSubject.LastModifiedTime = DateTime.UtcNow;
            existStudentSubject = studentSubjectRepository.Update(mapper.Map(studentSubjectForCreationDTO, existStudentSubject));

            await studentSubjectRepository.SaveChangesAsync();

            return mapper.Map<StudentSubjectForViewDTO>(existStudentSubject);
        }
    }
}
