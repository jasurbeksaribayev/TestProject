using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.Service.DTOs.StudentSubject;
using TestProject.Service.IServices.StudentSubjects;
using TestProject.Service.Services.Students;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IStudentSubjectService studentSubjectService;

        public StudentSubjectController(IStudentSubjectService studentSubjectService)
        {
            this.studentSubjectService = studentSubjectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentSubjectForCreationDTO studentSubjectForCreationDTO)
            => Ok(await studentSubjectService.CreateAsync(studentSubjectForCreationDTO));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, StudentSubjectForCreationDTO studentSubjectForCreationDTO)
            => Ok(await studentSubjectService.UpdateAsync(id, studentSubjectForCreationDTO));

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
            => Ok(await studentSubjectService.DeleteAsync(id));

        [HttpGet("byGetMaxScoreTeachers")]
        public async Task<IActionResult> GetByMaxScoreTeachersAsync()
            => Ok(await studentSubjectService.GetMaxScoreTeacherAsync());

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await studentSubjectService.GetAllAsync());   
    }
}
