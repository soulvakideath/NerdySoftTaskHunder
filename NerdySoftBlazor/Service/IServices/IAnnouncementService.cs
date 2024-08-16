using NerdySoftBlazor.Dto.AnnouncementDto;
using NerdySoftBlazor.Models;

namespace NerdySoftBlazor.Service.IServices;

public interface IAnnouncementService
{
    Task<ApiResult<List<AnnouncementDto>>> GetAsync(string token);
    Task<ApiResult<AnnouncementDto>> GetByIdAsync(Guid id, string token);
    Task<ApiResult<AnnouncementDto>> CreateAsync(CreateAnnouncementDto dto, string token);
    Task<ApiResult<bool>> UpdateAsync(Guid id, UpdateAnnouncementDto dto, string token);
    Task<ApiResult<bool>> DeleteAsync(Guid id, string token);
}