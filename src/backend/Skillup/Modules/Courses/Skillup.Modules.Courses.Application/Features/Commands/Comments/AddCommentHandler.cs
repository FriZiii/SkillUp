using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Comments;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Comments
{
    internal class AddCommentHandler(ICommentRepository commentRepository, IClock clock) : IRequestHandler<AddCommentRequest>
    {
        private readonly ICommentRepository commentRepository = commentRepository;
        private readonly IClock clock = clock;

        public async Task Handle(AddCommentRequest request, CancellationToken cancellationToken)
        {
            await commentRepository.Add(new Comment()
            {
                AuthorId = request.UserId,
                IsDeleted = false,
                Content = request.Content,
                ElementId = request.ElementId,
                ParentCommentId = request.ParentCommentId,
                CreatedAt = clock.CurrentDate(),
            });
        }
    }
}
