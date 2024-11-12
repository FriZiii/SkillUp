
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record AddArticleAssetRequest(Guid ElementId, IFormFile File) : IRequest
    {
        [JsonIgnore]
        public Guid Key { get; init; } = Guid.NewGuid();
    }
}
