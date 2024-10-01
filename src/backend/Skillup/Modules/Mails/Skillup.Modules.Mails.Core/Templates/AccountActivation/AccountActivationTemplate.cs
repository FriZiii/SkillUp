namespace Skillup.Modules.Mails.Core.Templates.AccountActivation
{
    internal class AccountActivationTemplate(int code) : ITemplate
    {
        private class AccountActivationModel(int code) : TemplateModel
        {
            public int Code { get; private set; } = code;
        }

        public string Subject { get; } = "Account Activation";
        public string Path { get; } = @"AccountActivation/account_activation.html";
        public TemplateModel Model { get; } = new AccountActivationModel(code);
    }
}
