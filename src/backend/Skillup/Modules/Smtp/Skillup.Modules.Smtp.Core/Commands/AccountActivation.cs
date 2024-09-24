using MediatR;

namespace Skillup.Modules.Mails.Core.Commands
{
    internal record AccountActivation(string Email) : IRequest
    {
    }
}
