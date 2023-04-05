using AutoMapper;
using MongoDB.Driver;
using TAO.Services.Catalog.Configurations;
using TAO.Services.Catalog.DTOs;
using TAO.Services.Catalog.DTOs.Crud_DTOs.Category;
using TAO.Services.Catalog.Model;
using TAO.Shared.DTOs;

namespace TAO.Services.Catalog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var newCategory = _mapper.Map<Category>(categoryCreateDto);
            await _categoryCollection.InsertOneAsync(newCategory);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), 204);
        }
        public async Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updateCategory = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == updateCategory.Id, updateCategory);
            if (result == null)
            {
                return Response<NoContent>.Fail("Category not found.", 404);
            }
            return Response<NoContent>.Success(204);
        }
        public async Task<Response<CategoryDto>> GetByIdAsync(string categoryId)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found.",404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);

        }
        
        
    }
}
