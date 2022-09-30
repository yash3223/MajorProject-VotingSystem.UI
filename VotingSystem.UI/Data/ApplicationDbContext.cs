using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartVotingSystem.UI.Models;
namespace SmartVotingSystem.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }

        public DbSet<PartiesMaster> PartiesMaster { get; set; }
        public DbSet<VoteMaster> VoteMaster { get; set; }
    }
}
