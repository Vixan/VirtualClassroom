using System;
using System.Collections.Generic;
using System.Text;
using VirtualClassroom.Domain;
using Microsoft.EntityFrameworkCore;

namespace VirtualClassroom.Persistence.EF
{
    public class DummyDbContext : DbContext
    {
        public DbSet<VirtualClassroom.Domain.Student> Students { get; set; }

        public DummyDbContext() { }

        public DummyDbContext(DbContextOptions<DummyDbContext> options)
            : base(options)
        {
        }
    }
}
