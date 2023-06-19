using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Service.DTOs.Subjects;
using TestProject.Service.IServices.Subjects;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(SubjectForCreationDTO subjectForCreationDTO)
            => Ok(await subjectService.CreateAsync(subjectForCreationDTO));

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
            => Ok(await subjectService.DeleteAsync(id));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, SubjectForCreationDTO subjectForCreationDTO)
            => Ok(await subjectService.UpdateAsync(id, subjectForCreationDTO));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await subjectService.GetAllAsync());
    }
}
