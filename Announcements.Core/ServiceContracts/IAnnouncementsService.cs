using Announcements.Core.Entities;

namespace Announcements.Core.ServiceContracts
{
    public interface IAnnouncementsService
    {
        Task<IEnumerable<Announcement>?> GetAnnouncementsAsync();
        Task<Announcement?> GetAnnouncementByIdAsync(int id);
        Task AddAnnouncementAsync(Announcement announcement);
        Task<Announcement?> UpdateAnnouncementAsync(int id, Announcement announcement);
        Task<bool> DeleteAnnouncementAsync(int id);
    }
}
