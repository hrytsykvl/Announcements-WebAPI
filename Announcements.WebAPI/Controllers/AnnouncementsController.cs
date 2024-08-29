using Announcements.Core.Entities;
using Announcements.Core.ServiceContracts;
using Announcements.Infrastructure.DatabaseContext;
using Announcements.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Announcements.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementsService _service;

        public AnnouncementsController(IAnnouncementsService service)
        {
            _service = service;
        }

        // GET: api/Announcements
        /// <summary>
        /// Get a list of announcements (including announcement ID, title, and date added) from the 'announcements' table.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            var announcements = await _service.GetAnnouncementsAsync();

            if (announcements == null)
            {
                return NotFound();
            }

            return announcements.ToList();
        }

        // GET: api/Announcements/5
        /// <summary>
        /// Get announcement details by announcement ID from the 'announcements' table
        /// </summary>
        /// <param name="announcementId">Announcement id to retrieve</param>
        /// <returns></returns>
        [HttpGet("{announcementId}")]
        public async Task<ActionResult<object>> GetAnnouncement(int announcementId)
        {
            var announcement = await _service.GetAnnouncementByIdAsync(announcementId);

            if (announcement == null)
            {
                return NotFound();
            }

            var announcements = await _service.GetAnnouncementsAsync();

            var similarAnnouncements = SimilarAnnouncements
                .GetSimilarAnnouncements(announcements.ToList(), announcement);

            var announcementWithSimilar = new
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Description = announcement.Description,
                DateAdded = announcement.DateAdded,
                SimilarAnnouncements = similarAnnouncements.Select(sa => new
                {
                    Id = sa.Id,
                    Title = sa.Title,
                    Description = sa.Description,
                    DateAdded = sa.DateAdded
                }).ToList()
            };

            return announcementWithSimilar;
        }

        // PUT: api/Announcements/5
        /// <summary>
        /// Update announcement details by announcement id in the 'announcements' table.
        /// </summary>
        /// <param name="announcementId">Announcement id to update</param>
        /// <param name="announcement">Announcement object to update</param>
        /// <returns></returns>
        [HttpPut("{announcementID}")]
        public async Task<IActionResult> PutAnnouncement(int announcementId, [Bind(nameof(Announcement.Id), nameof(Announcement.Title), nameof(Announcement.Description), nameof(Announcement.DateAdded))] Announcement announcement)
        {
            var updatedAnnouncement = await _service.UpdateAnnouncementAsync(announcementId, announcement);

            if (updatedAnnouncement == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Announcements
        /// <summary>
        /// Create and add a new announcement into the 'announcements' table.
        /// </summary>
        /// <param name="announcement">Created announcement object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Announcement>> PostAnnouncement([Bind(nameof(Announcement.Id), nameof(Announcement.Title),
            nameof(Announcement.Description), nameof(Announcement.DateAdded))] Announcement announcement)
        {
            announcement.DateAdded = DateTime.Now;
            await _service.AddAnnouncementAsync(announcement);

            return CreatedAtAction(nameof(GetAnnouncement), new { announcementId = announcement.Id }, announcement);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var isDeleted = await _service.DeleteAnnouncementAsync(id);
            if (!isDeleted)
            {
                return NotFound(); //HTTP 404
            }

            return NoContent(); //HTTP 200
        }
    }
}
