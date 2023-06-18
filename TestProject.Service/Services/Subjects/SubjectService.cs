using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.DTOs.Student;
using TestProject.Service.DTOs.Subject;
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
            var existSubject = await subjectRepository.GetAsync(s => s.Name.Equals(subjectForCreationDTO.Name));

            if (existSubject is not null)
                throw new TestProjectException(404, "this name already exist");

            var subject = await subjectRepository.CreateAsync(mapper.Map<Subject>(subjectForCreationDTO));
            await subjectRepository.SaveChangesAsync();

            return mapper.Map<SubjectForViewDTO>(subject);
        }

        public async Task<bool> DeletesAsync(int id)
        {
            var existSubject = await subjectRepository.DeleteAsync(id);

            if (!existSubject)
                throw new TestProjectException(404, "not found");

            await subjectRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<SubjectForViewDTO>> GetAllAsync()
        {
            var existSubject = subjectRepository.GetAll();

            if (existSubject is null)
                throw new TestProjectException(404, "not found");

            return mapper.Map<IQueryable<SubjectForViewDTO>>(existSubject);
        }

        public async Task<SubjectForViewDTO> UpdatesAsync(int id, SubjectForCreationDTO subjectForCreationDTO)
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
