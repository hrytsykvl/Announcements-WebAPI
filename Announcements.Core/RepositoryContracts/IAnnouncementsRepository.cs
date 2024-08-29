using Announcements.Core.Entities;

namespace Announcements.Core.RepositoryContracts
{
    public interface IAnnouncementsRepository
    {
        Task<IEnumerable<Announcement>> GetAnnouncementsAsync();
        Task<Announcement?> GetAnnouncementByIdAsync(int id);
        Task AddAnnouncementAsync(Announcement announcement);
        Task UpdateAnnouncementAsync();
        Task DeleteAnnouncementAsync(int id);
    }
}
