using Microsoft.EntityFrameworkCore;

namespace GuestService.Data
{
    public class AppDbContext
    {
        public class GuestContext : DbContext
        {
            public GuestContext(DbContextOptions<GuestContext> options) : base(options) { }

            public DbSet<Request> Requests { get; set; }
        }
    }
}
