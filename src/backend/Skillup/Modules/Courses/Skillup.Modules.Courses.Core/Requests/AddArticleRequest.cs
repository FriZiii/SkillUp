using MediatR;

namespace Skillup.Modules.Courses.Core.Requests
{
    public class AddArticleRequest : IRequest
    {
        public string Title { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublished { get; set; }
        public Guid SectionId { get; set; }
        public string HTMLContent { get; set; }
    }
}
