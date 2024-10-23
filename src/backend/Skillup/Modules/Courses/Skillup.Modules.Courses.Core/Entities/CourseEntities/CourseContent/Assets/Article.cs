namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets
{
    public class Article : Asset
    {
        public string HTMLContent { get; set; } //Content of the article as a HTML
        //right now article is stored in the database, might later move to server
    }
}
