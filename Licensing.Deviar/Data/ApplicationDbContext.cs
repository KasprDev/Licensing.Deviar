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
        public virtual DbSet<Reseller> Resellers { get; set; }
        public virtual DbSet<ResellerLog> ResellerLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsers(builder);

            builder.Entity<Software>()
                .HasMany(x => x.LicenseKeys)
                .WithOne(x => x.Software)
                .HasForeignKey(x => x.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Software>()
                .HasMany(x => x.Resellers)
                .WithOne(x => x.Software)
                .HasForeignKey(x => x.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(x => x.Software)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LicenseKey>()
                .HasOne(x => x.ResellerLog)
                .WithOne(x => x.LicenseKey)
                .HasForeignKey<ResellerLog>(x => x.LicenseKeyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Reseller>()
                .HasMany(x => x.Logs)
                .WithOne(x => x.Reseller)
                .HasForeignKey(x => x.ResellerId)
                .OnDelete(DeleteBehavior.NoAction);
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
            user.PasswordHash =
                passwordHasher.HashPassword(user, "u_h6-kUXVWHpKsizn9KbmiZii@9d9W.j9LmFru23UcY@QGW*xD@nf3M_kVzG");

            builder.Entity<AppUser>().HasData(user);
        }
    }
}