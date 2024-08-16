using NerdySoftApi.Dto.AnnouncementDto;
using NerdySoftApi.Models;

namespace NerdySoftApi.Services.IServices;

public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementDto>> GetAllAsync();
    Task<AnnouncementDto> GetByIdAsync(Guid id);
    Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto);
    Task<bool> UpdateAsync(UpdateAnnouncementDto dto);
    Task<bool> DeleteAsync(Guid id);
}