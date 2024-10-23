namespace Skillup.Modules.Mails.Core.Templates.AccountActivation
{
    internal class AccountActivationTemplate(Guid userId, Guid activationToken, DateTime tokenExpiration, string clientUrl) : ITemplate
    {
        private class AccountActivationModel(Guid userId, Guid activationToken, DateTime tokenExpiration, string clientUrl) : TemplateModel
        {
            public Guid UserId { get; } = userId;
            public Guid ActivationToken { get; } = activationToken;
            public DateTime TokenExpiration { get; } = tokenExpiration;
            public string ClientUrl { get; } = clientUrl;
        }

        public string Subject { get; } = "Account Activation";
        public string Path { get; } = @"AccountActivation/account_activation.html";
        public TemplateModel Model { get; } = new AccountActivationModel(userId, activationToken, tokenExpiration, clientUrl);
    }
}
