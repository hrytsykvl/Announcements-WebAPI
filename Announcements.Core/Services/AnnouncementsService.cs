using Announcements.Core.Entities;
using Announcements.Core.RepositoryContracts;
using Announcements.Core.ServiceContracts;

namespace Announcements.Core.Services
{
    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly IAnnouncementsRepository _repository;

        public AnnouncementsService(IAnnouncementsRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            await _repository.AddAnnouncementAsync(announcement);
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            await _repository.DeleteAnnouncementAsync(id);

            return true;
        }

        public async Task<Announcement?> GetAnnouncementByIdAsync(int id)
        {
            var announcement = await _repository.GetAnnouncementByIdAsync(id);

            if (announcement == null)
            {
                return null;
            }

            return announcement;
        }

        public async Task<IEnumerable<Announcement>?> GetAnnouncementsAsync()
        {
            var announcements = await _repository.GetAnnouncementsAsync();

            if (announcements == null)
            {
                return null;
            }

            return announcements;
        }

        public async Task<Announcement?> UpdateAnnouncementAsync(int id, Announcement announcement)
        {
            var existingAnnouncement = await _repository.GetAnnouncementByIdAsync(id);

            if (existingAnnouncement == null)
            {
                return null;
            }

            existingAnnouncement.Title = announcement.Title;
            existingAnnouncement.Description = announcement.Description;

            await _repository.UpdateAnnouncementAsync();

            return existingAnnouncement;
        }
    }
}
