using AutoMapper;
using TAO.Services.Catalog.DTOs;
using TAO.Services.Catalog.DTOs.Crud_DTOs.Category;
using TAO.Services.Catalog.Model;

namespace TAO.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            #region FeatureMappings
            CreateMap<Feature, FeatureDto>().ReverseMap();
            #endregion
            #region CourseMappings
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
            #endregion
            #region CategoryMappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            #endregion
        }
    }
}
