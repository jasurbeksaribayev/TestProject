using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Subjects;
using TestProject.Service.Exceptions;
using TestProject.Service.IServices.Subjects;

namespace TestProject.Service.Services.Subjects
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject> subjectRepository;
        private readonly IMapper mapper;

        public SubjectService(IGenericRepository<Subject> subjectRepository, IMapper mapper)
        {
            this.subjectRepository = subjectRepository;
            this.mapper = mapper;
        }

        public async Task<SubjectForViewDTO> CreateAsync(SubjectForCreationDTO subjectForCreationDTO)
        {
            var existSubject = await subjectRepository.GetAsync(s => s.Name.Equals(subjectForCreationDTO.Name) && s.TeacherId.Equals(subjectForCreationDTO.TeacherId));

            if (existSubject is not null)
                throw new TestProjectException(404, "this name already exist");

            var subject = await subjectRepository.CreateAsync(mapper.Map<Subject>(subjectForCreationDTO));
            await subjectRepository.SaveChangesAsync();

            return mapper.Map<SubjectForViewDTO>(subject);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existSubject = await subjectRepository.DeleteAsync(id);

            if (!existSubject)
                throw new TestProjectException(404, "not found");

            await subjectRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<SubjectForViewDTO>> GetAllAsync()
        {
            var existSubject = subjectRepository.GetAll().Include(s => s.Teacher).Include(s=>s.StudentSubjects).ThenInclude(s=>s.Student);

            if (existSubject is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IEnumerable<SubjectForViewDTO>>(existSubject);
        }

        public async Task<SubjectForViewDTO> UpdateAsync(int id, SubjectForCreationDTO subjectForCreationDTO)
        {
            var existSubject = await subjectRepository.GetAsync(s => s.Id.Equals(id));

            if (existSubject == null)
                throw new TestProjectException(404, "not found");

            var alreadyExistSubject = await subjectRepository.GetAsync(
                u => u.Name == subjectForCreationDTO.Name && u.Id != id);

            if (alreadyExistSubject != null)
                throw new TestProjectException(400, "this name already exists");

            existSubject.LastModifiedTime = DateTime.UtcNow;
            existSubject = subjectRepository.Update(mapper.Map(subjectForCreationDTO, existSubject));

            await subjectRepository.SaveChangesAsync();

            return mapper.Map<SubjectForViewDTO>(existSubject);
        }
    }
}
