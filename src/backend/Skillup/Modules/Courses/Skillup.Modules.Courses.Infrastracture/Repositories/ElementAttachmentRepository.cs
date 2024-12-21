using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class ElementAttachmentRepository : IElementAttachmentRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Attachment> _attachments;

        public ElementAttachmentRepository(CoursesDbContext context)
        {
            _context = context;
            _attachments = context.ElementAttachments;
        }

        public async Task Add(Attachment attachment)
        {
            await _attachments.AddAsync(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var attachmentToDelete = await _attachments.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception(); // TODO: Custom ex: attachment with id doesnt exist
            _attachments.Remove(attachmentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Attachment?> Get(Guid id)
            => await _attachments.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Attachment>> GetByCourseId(Guid courseId)
            => await _attachments.Where(x => x.Element.Section.CourseId == courseId).ToListAsync();

        public async Task<IEnumerable<Attachment>> GetByElementId(Guid elementId)
            => await _attachments.Where(x => x.ElementId == elementId).ToListAsync();
    }
}
