using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class AddArticleHandler : IRequestHandler<AddArticleRequest>
    {
        private readonly IElementRepository _elementRepository;

        public AddArticleHandler(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public async Task Handle(AddArticleRequest request, CancellationToken cancellationToken)
        {
            var element = new Element()
            {
                Title = request.Title,
                IsFree = request.IsFree,
                IsPublished = request.IsPublished,
                SectionId = request.SectionId,
                Asset = new Article()
                {
                    HTMLContent = request.HTMLContent,
                },
            };
            await _elementRepository.AddElement(element);
        }
    }
}
