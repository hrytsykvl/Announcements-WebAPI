using Announcements.Core.Entities;
using Announcements.Core.RepositoryContracts;
using Announcements.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Infrastructure.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            _context.Announcements.Add(announcement);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            var announcementToDelete = await _context.Announcements.FindAsync(id);

            _context.Announcements.Remove(announcementToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<Announcement?> GetAnnouncementByIdAsync(int id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task UpdateAnnouncementAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
