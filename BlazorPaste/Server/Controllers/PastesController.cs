using BlazorPaste.Server.Data;
using BlazorPaste.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorPaste.Server.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class PastesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PastesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<ActionResult<List<UserPasteItem>>> GetAllPastes()
        {
            var pastes = _context.Pastes;
            return Ok(await pastes.ToListAsync());
        }

        [HttpGet("getbyid")]
        [AllowAnonymous]
        public async Task<ActionResult<UserPasteItem>> GetById(int id)
        {
            var pastes = _context.Pastes;
            var selectedPaste = await pastes.FirstOrDefaultAsync(p => p.ID == id);
            if (selectedPaste is null) { return BadRequest(); }
            return Ok(selectedPaste);
        }

        [HttpGet]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserPasteItem>> CreatePaste(UserPasteItem paste)
        {
            var user = await _context.Users
                .Include(x => x.Pastes)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null) { return BadRequest(); }
            user.Pastes.Add(paste);
            _context.SaveChanges();
            return Ok();
        }
    }
}
