using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TestProject.Service.DTOs.Students;
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
        [Authorize(Roles="StudentPolicy")]
        public async Task<IActionResult> CreateAsync(StudentForCreationDTO studentForCreationDTO)
            => Ok(await studentService.CreateAsync(studentForCreationDTO));

        [Authorize("StudentPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, StudentForCreationDTO studentForCreationDTO)
            => Ok(await studentService.UpdateAsync(id, studentForCreationDTO));

        [Authorize("AdminPolicy")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
            => Ok(await studentService.DeleteAsync(id));

        [HttpGet("byAgeUnder20")]
        public async Task<IActionResult> GetByAgeAsync()
            => Ok(await studentService.GetByAgeAsync());

        [HttpGet("byDateBetweenAugust12ToSeptember18")]
        public async Task<IActionResult> GetByDateAsync()
            => Ok(await studentService.GetDateAsync());

        [HttpGet("bySearchFirstNameAndLastName")]
        public async Task<IActionResult> GetBySearchAsync(string search)
            => Ok(await studentService.GetSearchAsync(s=>s.FirstName.Contains(search) || s.LastName.Contains(search)));

        [HttpGet("byGetMaxScoreSubject")]
        public async Task<IActionResult> GetByMaxScoreSubjectAsync(int id)
            => Ok(await studentService.GetMaxScoreSubjectAsync(id));

        [HttpGet("byGetMobileNumberStartWith90And91Async")]
        public async Task<IActionResult> GetByMobileNumberStartWith90And91Async()
            => Ok(await studentService.GetMobileNumberStartWith90And91Async());

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await studentService.GetAllAsync());
    }
}
