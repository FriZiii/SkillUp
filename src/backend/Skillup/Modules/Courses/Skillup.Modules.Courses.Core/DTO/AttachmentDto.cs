namespace Skillup.Modules.Courses.Core.DTO
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }
}
