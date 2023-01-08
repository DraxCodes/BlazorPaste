using BlazorPaste.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorPaste.Server.Data
{
    public class AnonymousDbContext : DbContext
    {
        public AnonymousDbContext(DbContextOptions<AnonymousDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserPasteItem> AllUserPastes => Set<UserPasteItem>();
    }
}
