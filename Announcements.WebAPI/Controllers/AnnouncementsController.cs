using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Announcements.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello";
        }
    }
}
