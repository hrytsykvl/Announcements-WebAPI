using Announcements.Core.Entities;
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
        private readonly ApplicationDbContext _context;

        public AnnouncementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Announcements
        /// <summary>
        /// Get a list of announcements (including announcement ID, title, and date added) from the 'announcements' table.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            if (_context.Announcements == null)
            {
                return NotFound();
            }

            return await _context.Announcements
                .OrderBy(announcement => announcement.DateAdded)
                .ToListAsync();
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
            if (_context.Announcements == null)
            {
                return NotFound();
            }
            var announcement = await _context.Announcements.FirstOrDefaultAsync(a => a.Id == announcementId);

            if (announcement == null)
            {
                return NotFound();
            }

            var similarAnnouncements = SimilarAnnouncements
                .GetSimilarAnnouncements(await _context.Announcements.ToListAsync(), announcement);

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
            if (announcementId != announcement.Id)
            {
                return BadRequest();
            }

            var existingAnnouncement = await _context.Announcements.FindAsync(announcementId);
            if (existingAnnouncement == null)
            {
                return NotFound();
            }

            existingAnnouncement.Title = announcement.Title;
            existingAnnouncement.Description = announcement.Description;
            existingAnnouncement.DateAdded = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(announcementId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
            if (_context.Announcements == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Announcements' is null.");
            }

            announcement.DateAdded = DateTime.Now;
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnouncement", new { announcementId = announcement.Id }, announcement);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Announcements.FindAsync(id);
            if (city == null)
            {
                return NotFound(); //HTTP 404
            }

            _context.Announcements.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent(); //HTTP 200
        }

        private bool AnnouncementExists(int id)
        {
            return (_context.Announcements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
