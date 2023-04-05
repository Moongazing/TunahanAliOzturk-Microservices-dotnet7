using TAO.Services.Catalog.DTOs;
using TAO.Services.Catalog.DTOs.Crud_DTOs.Category;
using TAO.Shared.DTOs;

namespace TAO.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<Response<CategoryDto>> GetByIdAsync(string categoryId);
        Task<Response<NoContent>> DeleteAsync(string categoryId);
    }
}
