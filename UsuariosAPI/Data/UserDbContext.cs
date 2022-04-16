using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDbContext(
            DbContextOptions<UserDbContext> options, 
            IConfiguration configuration
        ) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IdentityUser<int> admin = new IdentityUser<int>()
            {
                UserName = _configuration.GetValue<string>("DatabaseSettings:DefaultAdminName"),
                NormalizedUserName = _configuration
                    .GetValue<string>("DatabaseSettings:DefaultAdminName")
                    .ToUpper(),
                Email = _configuration.GetValue<string>("DatabaseSettings:DefaulAdminMail"),
                NormalizedEmail = _configuration
                    .GetValue<string>("DatabaseSettings:DefaulAdminMail")
                    .ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 1
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

            admin.PasswordHash = hasher.HashPassword(
                admin,
                _configuration.GetValue<string>("DatabaseSettings:DefaultAdminPassword")
            );


            builder.Entity<IdentityUser<int>>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 2, Name = "user", NormalizedName = "USER" }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
            );
        }
    }
}
