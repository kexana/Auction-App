using AuctionApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AuctionApp.Seed
{
    public static class DatabaseSeedUtilities
    {
        public static void SeedRoles(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                using (var devLoungeDbContext = serviceScope.ServiceProvider.GetRequiredService<AuctionAppDbContext>())
                {
                    devLoungeDbContext.Database.Migrate();

                    if (devLoungeDbContext.Roles.ToList().Count == 0)
                    {
                        IdentityRole adminRole = new IdentityRole();
                        adminRole.Name = "Admin";
                        adminRole.NormalizedName = adminRole.Name.ToUpper();

                        IdentityRole moderatorRole = new IdentityRole();
                        moderatorRole.Name = "Moderator";
                        moderatorRole.NormalizedName = moderatorRole.Name.ToUpper();

                        IdentityRole userRole = new IdentityRole();
                        userRole.Name = "User";
                        userRole.NormalizedName = userRole.Name.ToUpper();

                        devLoungeDbContext.Add(adminRole);
                        devLoungeDbContext.Add(moderatorRole);
                        devLoungeDbContext.Add(userRole);

                        devLoungeDbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
