using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Mails.Core.Templates.PasswordChange
{
    internal class PasswordChangedTemplate(Email skillupEmail) : ITemplate
    {
        private class PasswordChangedModel(Email skillupEmail) : TemplateModel
        {
            public Email SkillUpEmail { get; set; } = skillupEmail;
        }

        public string Subject => "Your password has been changed";
        public string Path => @"PasswordChange/password_changed.html";
        public TemplateModel Model => new PasswordChangedModel(skillupEmail);
    }
}
