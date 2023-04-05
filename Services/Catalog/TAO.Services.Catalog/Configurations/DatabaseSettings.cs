namespace TAO.Services.Catalog.Configurations
{
    public class DatabaseSettings : IDatabaseSettings
    {
        string IDatabaseSettings.CourseCollectionName { get; set; }
        string IDatabaseSettings.CategoryCollectionName { get; set; }
        string IDatabaseSettings.ConnectionString { get; set; }
        string IDatabaseSettings.DatabaseName { get; set; }
    }
}
