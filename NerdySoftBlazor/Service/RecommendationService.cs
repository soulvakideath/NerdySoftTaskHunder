using NerdySoftBlazor.Dto.AnnouncementDto;
using NerdySoftBlazor.Models;
using NerdySoftBlazor.Service.IServices;

namespace NerdySoftBlazor.Service;

public class RecommendationService : IRecommendationService
{
    private readonly HttpService _httpService;
    private readonly string _baseUrl = "api/recommendation";

    public RecommendationService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ApiResult<List<AnnouncementDto>>> GetSimilar(Guid id, string token)
    {
        var url = $"{_baseUrl}/{id}";
        var result = await _httpService.GetAsync<List<AnnouncementDto>>(url, token);
        return result.IsSuccess ? result : ApiResult<List<AnnouncementDto>>.Failure(result.ErrorMessage);
    }
}