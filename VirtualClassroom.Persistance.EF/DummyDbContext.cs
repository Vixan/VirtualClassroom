using Microsoft.EntityFrameworkCore;

namespace VirtualClassroom.Persistence.EF
{
    public class DummyDbContext : DbContext
    {
        public DummyDbContext() { }

        public DummyDbContext(DbContextOptions<DummyDbContext> options)
            : base(options)
        {
        }

        public DbSet<VirtualClassroom.Domain.Student> Students { get; set; }
        public DbSet<VirtualClassroom.Domain.Professor> Professors { get; set; }
        public DbSet<VirtualClassroom.Domain.Activity> Activities { get; set; }
    }
}
