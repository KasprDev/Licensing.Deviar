using Licensing.Deviar;
using Licensing.Deviar.Data;
using Microsoft.AspNetCore.Identity;

internal static class DbInitializerExtension
{
    public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userMgr = services.GetRequiredService<UserManager<AppUser>>();

            DbInit.InitializeDb(context, userMgr);
        }
        catch (Exception ex)
        {

        }

        return app;
    }
}