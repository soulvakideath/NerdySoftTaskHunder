using NerdySoftApi.Dto.AnnouncementDto;

namespace NerdySoftApi.Services.IServices;

public interface IRecommendationService
{
    Task<IEnumerable<AnnouncementDto>> FindSimilarAsync(Guid id);
}