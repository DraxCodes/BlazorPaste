using BlazorPaste.Server.Data;
using BlazorPaste.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorPaste.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PastesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PastesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserPasteItem>>> GetPastes()
        {
            var user = await _context.Users
                .Include(x => x.Pastes)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user.Pastes);
        }
    }
}
