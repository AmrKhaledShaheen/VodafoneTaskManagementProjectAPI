using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.DataContext
{
    public class DbContextTask : IdentityDbContext<VodafoneUser>
    {

        public DbSet<VodafoneUser> VodafoneUsers { get; set; }
        public DbSet<Domain.Entities.Tasks> Tasks { get; set; }
        public DbSet<Subscriptions> Subscriptions{ get; set; }
        public DbContextTask(DbContextOptions<DbContextTask> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
