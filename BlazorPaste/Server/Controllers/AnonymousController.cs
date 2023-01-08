using BlazorPaste.Server.Data;
using BlazorPaste.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPaste.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AnonymousController : ControllerBase
    {
        private readonly AnonymousDbContext _anonymousDbContext;

        public AnonymousController(AnonymousDbContext anonymousDbContext)
        {
            _anonymousDbContext = anonymousDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserPasteItem>>> GetAllPastes()
        {
            var pastesQuery = _anonymousDbContext.AllUserPastes;

            if (pastesQuery.Any())
            {
                var pastes = await pastesQuery.ToListAsync();
                return Ok(pastes);
            }
            else { return BadRequest(); }
        }
    }
}
