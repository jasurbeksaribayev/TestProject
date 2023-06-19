using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Service.DTOs.Teacher;
using TestProject.Service.IServices.Teachers;
using TestProject.Service.Services.Students;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TeacherForCreationDTO teacherForCreationDTO)
            => Ok(await teacherService.CreateAsync(teacherForCreationDTO));

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
            => Ok(await teacherService.DeleteAsync(id));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, TeacherForCreationDTO teacherForCreationDTO)
            => Ok(await teacherService.UpdateAsync(id, teacherForCreationDTO));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await teacherService.GetAllAsync());

        [HttpGet("byAgeOver55")]
        public async Task<IActionResult> GetByAgeAsync()
            => Ok(await teacherService.GetByAgeAsync());

        [HttpGet("byGetMobileNumberStartWith90And91Async")]
        public async Task<IActionResult> GetByMobileNumberStartWith90And91Async()
            => Ok(await teacherService.GetMobileNumberStartWith90And91Async());
    }
}
