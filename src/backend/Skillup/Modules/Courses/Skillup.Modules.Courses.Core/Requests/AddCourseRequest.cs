using MediatR;

namespace Skillup.Modules.Courses.Core.Requests
{
    public class AddCourseRequest : IRequest
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //author
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }
}
