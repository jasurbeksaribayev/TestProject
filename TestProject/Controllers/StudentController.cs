using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.Service.DTOs.Student;
using TestProject.Service.IServices.Students;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentForCreationDTO studentForCreationDTO)
            => Ok(studentService.CreateAsync(studentForCreationDTO));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, StudentForCreationDTO studentForCreationDTO)
            => Ok(studentService.UpdatesAsync(id, studentForCreationDTO));

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
            => Ok(studentService.DeletesAsync(id));

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetAgeAsync()
            => Ok(studentService.GetAgeAsync());
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(studentService.GetAllAsync());
    }
}
