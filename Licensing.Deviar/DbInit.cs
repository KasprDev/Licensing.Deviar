using Licensing.Deviar.Data;
using Microsoft.AspNetCore.Identity;

namespace Licensing.Deviar
{
    public class DbInit
    {
        public static async void InitializeDb(ApplicationDbContext context, UserManager<AppUser> userMgr)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            // Don't run if there's any existing users.
            if (context.Users.Any()) return;

            var user = new AppUser()
            {
                UserName = "contact@deviar.net",
                Email = "contact@deviar.net"
            };

            var identity = await userMgr.CreateAsync(user, "Picturesque1@");

            await context.SaveChangesAsync();
        }
    }
}
