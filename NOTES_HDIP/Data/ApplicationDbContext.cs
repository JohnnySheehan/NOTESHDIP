using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NOTES_HDIP.Models;

namespace NOTES_HDIP.Data
{
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<NoteSpace> NoteSpaces { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<CommunityComment> CommunityComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.NoteSpace).WithOne(u => u.User).IsRequired();
          
            base.OnModelCreating(builder);
        }
    }
}