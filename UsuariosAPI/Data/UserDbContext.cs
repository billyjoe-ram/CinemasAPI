using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {

        public UserDbContext(
            DbContextOptions<UserDbContext> options
        ) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var configurations = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            IdentityUser<int> admin = new IdentityUser<int>()
            {
                UserName = configurations.GetValue<string>("DatabaseSettings:DefaultAdminName"),
                NormalizedUserName = configurations
                    .GetValue<string>("DatabaseSettings:DefaultAdminName")
                    .ToUpper(),
                Email = configurations.GetValue<string>("DatabaseSettings:DefaulAdminMail"),
                NormalizedEmail = configurations
                    .GetValue<string>("DatabaseSettings:DefaulAdminMail")
                    .ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 1
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

            admin.PasswordHash = hasher.HashPassword(
                admin,
                configurations.GetValue<string>("DatabaseSettings:DefaultAdminPassword")
            );


            builder.Entity<IdentityUser<int>>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
            );
        }
    }
}
