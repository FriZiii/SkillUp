using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record class EditUserPrivacySettingsRequest(bool IsAccountPublicForLoggedInUsers, bool ShowCoursesOnUserProfile) : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
