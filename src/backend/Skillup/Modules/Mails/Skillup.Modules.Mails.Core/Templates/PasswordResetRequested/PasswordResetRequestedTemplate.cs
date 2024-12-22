namespace Skillup.Modules.Mails.Core.Templates.PasswordReset
{
    internal class PasswordResetRequestedTemplate(string token, string clientUrl) : ITemplate
    {
        private class PasswordResetRequestedModel(string passwordResetToken, string clientUrl) : TemplateModel
        {
            public string PasswordResetToken { get; } = passwordResetToken;
            public string ClientUrl { get; } = clientUrl;
        }

        public string Subject => "Password reset";
        public string Path => @"PasswordResetRequested/password_reset_requested.html";
        public TemplateModel Model => new PasswordResetRequestedModel(token, clientUrl);
    }
}
