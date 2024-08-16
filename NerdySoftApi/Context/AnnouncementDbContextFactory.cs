using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace NerdySoftApi.Context;

public class AnnouncementDbContextFactory : IDesignTimeDbContextFactory<AnnouncementContext>
{
    public AnnouncementContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AnnouncementContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new AnnouncementContext(optionsBuilder.Options);
    }
}