using Announcements.Core.Entities;

namespace Announcements.Infrastructure.Helpers
{
    public static class SimilarAnnouncements
    {
        public static List<Announcement> GetSimilarAnnouncements(List<Announcement> announcements, Announcement currentAnnouncement)
        {
            return announcements
                   .Where(announcement => announcement.Id != currentAnnouncement.Id &&
                       (announcement.Title.Split(' ').Intersect(currentAnnouncement.Title.Split(' ')).Any() &&
                        announcement.Description.Split(' ').Intersect(currentAnnouncement.Description.Split(' ')).Any()))
                   .TakeLast(3)
                   .ToList();
        }
    }
}
