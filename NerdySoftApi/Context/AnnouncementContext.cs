using Microsoft.EntityFrameworkCore;
using NerdySoftApi.Models;


namespace NerdySoftApi.Context;

public class AnnouncementContext : DbContext
{
    public AnnouncementContext(DbContextOptions<AnnouncementContext> options) : base(options) { }
    public DbSet<Announcement> Announcements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}