namespace Skillup.Modules.Mails.Core.Templates
{
    internal interface ITemplate
    {
        public string Subject { get; }
        public string Path { get; }
        public TemplateModel Model { get; }
    }
}
