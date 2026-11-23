using System.Linq;
using CTRL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("DefaultConnection not found");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    // ONLY Admin + Organiser can manage Events
    options.AddPolicy("EventManagersOnly", policy =>
        policy.RequireRole("Admin", "Organiser"));

    // ONLY Admin + DJ can manage DJs
    options.AddPolicy("DJManagersOnly", policy =>
        policy.RequireRole("Admin", "DJ"));
});

builder.Services.AddRazorPages(options =>
{
    // Protect Event management pages
    options.Conventions.AuthorizePage("/Events/Create", "EventManagersOnly");
    options.Conventions.AuthorizePage("/Events/Edit", "EventManagersOnly");
    options.Conventions.AuthorizePage("/Events/Delete", "EventManagersOnly");

    // Protect DJ management pages
    options.Conventions.AuthorizePage("/DJs/Create", "DJManagersOnly");
    options.Conventions.AuthorizePage("/DJs/Edit", "DJManagersOnly");
    options.Conventions.AuthorizePage("/DJs/Delete", "DJManagersOnly");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "Organiser", "DJ", "Attendee"};

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // First registered user becomes Admin automatically
    var admins = await userManager.GetUsersInRoleAsync("Admin");

    if (admins.Count == 0)
    {
        var firstUser = await userManager.Users
            .OrderBy(u => u.Email)
            .FirstOrDefaultAsync();

        if (firstUser != null)
        {
            await userManager.AddToRoleAsync(firstUser, "Admin");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();