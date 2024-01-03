using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataSeeding
{
    public static class UserSeeding
    {
        public static PasswordHasher<User> PasswordHasher = new PasswordHasher<User>();
        public static ModelBuilder SeedUsers(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "86c55eee-8933-11ee-b9d1-0242ac120002",
                    NationalId = "30009042700853",
                    UserName = "admin@marasim.com",
                    Email = "admin@marasim.com",
                    NormalizedEmail = "ADMIN@MARASIM.COM",
                    NormalizedUserName = "ADMIN@MARASIM.COM",
                    Gender = true,
                    PhoneNumber = "01100233249",
                    Name = "Admin",
                    PasswordHash = PasswordHasher.HashPassword(null, "Asd@12345"),
                    PicUrl = string.Empty
                }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "8433688c-a3d9-442b-8e2b-31cc61ca02c2",
                UserId = "86c55eee-8933-11ee-b9d1-0242ac120002"
            });
            return builder;
        }
    }
}
