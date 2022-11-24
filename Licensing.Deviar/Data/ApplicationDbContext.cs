using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Licensing.Deviar.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<LicenseKey> LicenseKeys { get; set; }
        public virtual DbSet<Software> Software { get; set; }
        public virtual DbSet<UsageLog> UsageLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            this.SeedUsers(builder);

            builder.Entity<Software>()
                .HasMany(x => x.LicenseKeys)
                .WithOne(x => x.Software)
                .HasForeignKey(x => x.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(x => x.Software)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            var user = new AppUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "contact@deviar.net",
                Email = "contact@deviar.net"
            };

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Picturesque1@");

            builder.Entity<AppUser>().HasData(user);
        }
    }
}