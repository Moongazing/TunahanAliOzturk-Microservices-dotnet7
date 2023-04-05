using AutoMapper;
using MongoDB.Driver;
using TAO.Services.Catalog.Configurations;
using TAO.Services.Catalog.DTOs;
using TAO.Services.Catalog.DTOs.Crud_DTOs.Category;
using TAO.Services.Catalog.Model;
using TAO.Shared.DTOs;

namespace TAO.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(courses => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x=>x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourse.Id, updateCourse);
            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found.", 404);
            }
            return Response<NoContent>.Success(200);
        }
        public async Task<Response<CourseDto>> GetByIdAsync(string courseId)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == courseId).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found.", 404);
            }
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);

        }
        public async Task<Response<NoContent>> DeleteAsync(string courseId)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == courseId);
            if (result.DeletedCount > 0 )
            {
                return Response<NoContent>.Success(200);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found.",404);
            }
        }
        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x=>x.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
    }
}
