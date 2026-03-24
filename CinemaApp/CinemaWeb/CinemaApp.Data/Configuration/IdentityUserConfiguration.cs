using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();

            IdentityUser user = new IdentityUser
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "seeduser@cinemaapp.com",
                NormalizedUserName = "SEEDUSER@CINEMAAPP.COM",
                Email = "seeduser@cinemaapp.com",
                NormalizedEmail = "SEEDUSER@CINEMAAPP.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "123456");

            builder.HasData(user);
        }
    }
}