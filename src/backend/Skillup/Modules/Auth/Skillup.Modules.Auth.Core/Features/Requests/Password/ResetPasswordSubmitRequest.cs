using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Requests.Password
{
    public record ResetPasswordSubmitRequest : IRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
