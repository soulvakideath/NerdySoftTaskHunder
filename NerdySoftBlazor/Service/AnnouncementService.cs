using NerdySoftBlazor.Dto.AnnouncementDto;
using NerdySoftBlazor.Models;
using NerdySoftBlazor.Service.IServices;

namespace NerdySoftBlazor.Service;

public class AnnouncementService : IAnnouncementService
{
    private readonly HttpService _httpService;
    private readonly string _baseUrl = "api/announcement";

    public AnnouncementService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ApiResult<List<AnnouncementDto>>> GetAsync(string token)
    {
        var result = await _httpService.GetAsync<List<AnnouncementDto>>(_baseUrl, token);
        return result.IsSuccess ? result : ApiResult<List<AnnouncementDto>>.Failure(result.ErrorMessage);
    }

    public async Task<ApiResult<AnnouncementDto>> GetByIdAsync(Guid id, string token)
    {
        var result = await _httpService.GetAsync<AnnouncementDto>($"{_baseUrl}/{id}", token);
        return result.IsSuccess ? result : ApiResult<AnnouncementDto>.Failure(result.ErrorMessage);
    }
    
    public async Task<ApiResult<AnnouncementDto>> CreateAsync(CreateAnnouncementDto dto, string token)
    {
        var result = await _httpService.PostAsync<AnnouncementDto>(_baseUrl, dto, token);
        return result.IsSuccess ? result : ApiResult<AnnouncementDto>.Failure(result.ErrorMessage);
    }

    public async Task<ApiResult<bool>> UpdateAsync(Guid id, UpdateAnnouncementDto dto, string token)
    {
        var result = await _httpService.PutAsync<bool>($"{_baseUrl}/{id}", dto, token);
        return result.IsSuccess ? result : ApiResult<bool>.Failure(result.ErrorMessage);
    }

    public async Task<ApiResult<bool>> DeleteAsync(Guid id, string token)
    {
        var result = await _httpService.DeleteAsync<bool>($"{_baseUrl}/{id}", token);
        return result.IsSuccess ? result : ApiResult<bool>.Failure(result.ErrorMessage);
    }
}