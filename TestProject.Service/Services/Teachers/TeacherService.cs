using AutoMapper;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Teacher;
using TestProject.Service.Exceptions;
using TestProject.Service.IServices.Teachers;

namespace TestProject.Service.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly IGenericRepository<Teacher> teacherRepository;
        private readonly IMapper mapper;

        public TeacherService(IGenericRepository<Teacher> teacherRepository, IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
        }

        public async Task<TeacherForViewDTO> CreateAsync(TeacherForCreationDTO teacherForCreationDTO)
        {
            var existTeacher = await teacherRepository.GetAsync(s => s.Email.Equals(teacherForCreationDTO.Email));

            if (existTeacher is not null)
                throw new TestProjectException(404, "this email already exist");

            var teacher = await teacherRepository.CreateAsync(mapper.Map<Teacher>(teacherForCreationDTO));
            await teacherRepository.SaveChangesAsync();

            return mapper.Map<TeacherForViewDTO>(teacher);
        }

        public async Task<bool> DeletesAsync(int id)
        {
            var existTeacher = await teacherRepository.DeleteAsync(id);

            if (!existTeacher)
                throw new TestProjectException(404, "not found");

            await teacherRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TeacherForViewDTO>> GetAgeAsync()
        {
            var existTeacherAgeFromFiftyFive = teacherRepository.GetAll(s => DateTime.Now.Year - s.BirthDate.Year >= 55);

            if (existTeacherAgeFromFiftyFive is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IQueryable<TeacherForViewDTO>>(existTeacherAgeFromFiftyFive);
        }

        public async Task<IEnumerable<TeacherForViewDTO>> GetAllAsync()
        {
            var existTeacher = teacherRepository.GetAll();

            if (existTeacher is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IQueryable<TeacherForViewDTO>>(existTeacher);
        }

        public async Task<TeacherForViewDTO> UpdatesAsync(int id, TeacherForCreationDTO teacherForCreationDTO)
        {
            var existTeacher = await teacherRepository.GetAsync(s => s.Id.Equals(id));

            if (existTeacher == null)
                throw new TestProjectException(404, "not found");

            var alreadyExistTeacher = await teacherRepository.GetAsync(u => u.Email == teacherForCreationDTO.Email && u.Id != id);

            if (alreadyExistTeacher != null)
                throw new TestProjectException(400, "this email already exists");

            existTeacher.LastModifiedTime = DateTime.UtcNow;
            existTeacher = teacherRepository.Update(mapper.Map(teacherForCreationDTO, existTeacher));

            await teacherRepository.SaveChangesAsync();

            return mapper.Map<TeacherForViewDTO>(existTeacher);
        }
    }
}
