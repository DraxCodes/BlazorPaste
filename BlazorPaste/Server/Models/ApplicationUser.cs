using BlazorPaste.Shared;
using Microsoft.AspNetCore.Identity;

namespace BlazorPaste.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserPasteItem> Pastes { get; set; } = new List<UserPasteItem>();
    }
}