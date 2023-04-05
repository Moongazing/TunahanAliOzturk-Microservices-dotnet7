using TAO.Services.Catalog.DTOs;
using TAO.Shared.DTOs;

namespace TAO.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<CourseDto>> GetByIdAsync(string courseId);
    }
}
