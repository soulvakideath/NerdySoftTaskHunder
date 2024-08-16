using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NerdySoftApi.Context;
using NerdySoftApi.Dto.AnnouncementDto;
using NerdySoftApi.Services.IServices;

namespace NerdySoftApi.Services;

public class RecommendationService : IRecommendationService
{
    private readonly AnnouncementContext _context;

    public RecommendationService(AnnouncementContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AnnouncementDto>> FindSimilarAsync(Guid id)
    {
        var targetAnnouncement = await _context.Announcements.FindAsync(id);

        if (targetAnnouncement == null)
        {
            return Enumerable.Empty<AnnouncementDto>();
        }

        var input = Tokenize(targetAnnouncement.Title + " " + targetAnnouncement.Description);

        var announcements = await _context.Announcements.ToListAsync();
        var similarAnnouncements = announcements.Select(a => new
            {
                Announcement = a,
                SimilarityScore = Tokenize(a.Title + " " + a.Description)
                    .Intersect(input, StringComparer.OrdinalIgnoreCase)
                    .Count()
            })
            .Where(x => x.SimilarityScore > 0)
            .OrderByDescending(x => x.SimilarityScore)
            .Take(3)
            .Select(x => new AnnouncementDto
            {
                Id = x.Announcement.Id,
                Title = x.Announcement.Title,
                Description = x.Announcement.Description,
                Date = x.Announcement.Date
            });

        return similarAnnouncements;
    }

    private static HashSet<string> Tokenize(string text)
    {
        var words = Regex.Split(text, @"\W+")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return words;
    }
}