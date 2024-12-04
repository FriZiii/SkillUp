using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class FillTheGapRepository : IFillTheGapRepository
    {

        private readonly CoursesDbContext _context;
        private readonly DbSet<FillTheGapSentence> _sentences;
        private readonly DbSet<FillTheGapWord> _words;
        public FillTheGapRepository(CoursesDbContext context)
        {
            _context = context;
            _sentences = context.FillTheGapSentences;
            _words = context.FillTheGapWords;
        }

        public async Task AddSentence(FillTheGapSentence fillTheGapSentence)
        {
            _sentences.Add(fillTheGapSentence);
            await _context.SaveChangesAsync();
        }

        public async Task AddWords(List<FillTheGapWord> words)
        {
            foreach (FillTheGapWord word in words)
            {
                _words.Add(word);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FillTheGapSentence>> GetFillTheGaps(Guid assignmentId)
        {
            var sentences = await _sentences.Include(s => s.Words).Where(s => s.AssignmentId == assignmentId)
                .ToListAsync();
            return sentences;
        }
    }
}
