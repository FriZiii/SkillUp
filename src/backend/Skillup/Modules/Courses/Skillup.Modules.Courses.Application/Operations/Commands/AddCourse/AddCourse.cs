using MediatR;

namespace Skillup.Modules.Courses.Application.Operations.Commands.AddCourse
{
    public class AddCourse : IRequest
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //author
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }
}
