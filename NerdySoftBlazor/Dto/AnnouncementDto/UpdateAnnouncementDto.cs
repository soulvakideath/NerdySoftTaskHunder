namespace NerdySoftBlazor.Dto.AnnouncementDto;

public class UpdateAnnouncementDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Date { get; set; }
}