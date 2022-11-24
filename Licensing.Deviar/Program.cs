using System.Text;
using Licensing.Deviar;
using Licensing.Deviar.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "licensing.deviar.net",
        AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
        ValidAudience = "licensing.deviar.net",
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("JpIpw4UYLkiaQZC2mymLZ82AIa3niKRDVffEyImoDWCI6hOC2Ev7M0d5pFxm"))
    };
});

builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(opt => opt.RootPath = Path.Combine(builder.Environment.WebRootPath, "dist"));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters = string.Empty;
    })
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSpa(spa => { spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200"); });
}
else
{
    app.UseSpaStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "dist"))
    });

    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "";
        spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "dist"))
        };
    });
}

app.Run();
