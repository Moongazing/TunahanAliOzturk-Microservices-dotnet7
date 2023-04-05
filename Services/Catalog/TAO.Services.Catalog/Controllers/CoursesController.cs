using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAO.Services.Catalog.DTOs;
using TAO.Services.Catalog.Services;
using TAO.Shared.BaseController;

namespace TAO.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetById(string courseId)
        {
            var response = await _courseService.GetByIdAsync(courseId);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateAsync(courseUpdateDto);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string courseId)
        {
            var response = await _courseService.DeleteAsync(courseId);
            return CreateActionResultInstance(response);
        }
    }
}
