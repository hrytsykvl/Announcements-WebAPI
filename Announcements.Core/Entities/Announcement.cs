using System.ComponentModel.DataAnnotations;

namespace Announcements.Core.Entities
{
    public class Announcement
    {
        [Key]
        public string Title { get; set; }
        public string Desciption { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
