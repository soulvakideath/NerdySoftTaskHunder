using NerdySoftBlazor.Dto.AnnouncementDto;
using NerdySoftBlazor.Models;

namespace NerdySoftBlazor.Service.IServices;

public interface IRecommendationService
{
    Task<ApiResult<List<AnnouncementDto>>> GetSimilar(Guid id, string token);
}