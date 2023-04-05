using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAO.Services.Catalog.DTOs;
using TAO.Services.Catalog.DTOs.Crud_DTOs.Category;
using TAO.Services.Catalog.Services;
using TAO.Shared.BaseController;

namespace TAO.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(string categoryId)
        {
            var response = await _categoryService.GetByIdAsync(categoryId);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryService.CreateAsync(categoryCreateDto);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _categoryService.UpdateAsync(categoryUpdateDto);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var response = await _categoryService.DeleteAsync(categoryId);
            return CreateActionResultInstance(response);
        }

    }
}
