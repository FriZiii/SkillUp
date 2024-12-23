namespace Skillup.Modules.Courses.Core.DTO
{
    public class AttachmentFileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }

    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public Guid ElementId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
