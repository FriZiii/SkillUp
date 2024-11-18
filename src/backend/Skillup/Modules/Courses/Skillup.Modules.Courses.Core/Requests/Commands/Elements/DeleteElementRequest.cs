
using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record DeleteElementRequest(Guid ElementId) : IRequest;
}
