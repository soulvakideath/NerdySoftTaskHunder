using Microsoft.EntityFrameworkCore;
using NerdySoftApi.Context;
using NerdySoftApi.Dto.AnnouncementDto;
using NerdySoftApi.Models;
using NerdySoftApi.Services.IServices;

namespace NerdySoftApi.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly AnnouncementContext _context;

    public AnnouncementService(AnnouncementContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
    {
        return await _context.Announcements
            .Select(announcement => new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Description = announcement.Description,
                Date = announcement.Date
            }).ToListAsync();
    }

    public async Task<AnnouncementDto> GetByIdAsync(Guid id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement is null) return null;

        return new AnnouncementDto
        {
            Id = announcement.Id,
            Title = announcement.Title,
            Description = announcement.Description,
            Date = announcement.Date
        };
    }

    public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto)
    {
        var announcement = new Announcement
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date
        };

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        return new AnnouncementDto
        {
            Id = announcement.Id,
            Title = announcement.Title,
            Description = announcement.Description,
            Date = announcement.Date
        };
    }
    
    public async Task<bool> UpdateAsync(UpdateAnnouncementDto dto)
    {
        var existingOperation = await _context.Announcements.FindAsync(dto.Id);
        if (existingOperation == null) return false;

        existingOperation.Title = dto.Title;
        existingOperation.Description = dto.Description;
        existingOperation.Date = dto.Date;

        _context.Announcements.Update(existingOperation);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement is null) return false;

        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();

        return true;
    }
}