using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataSeeding
{
    public static class RoleSeeding
    {
        public static ModelBuilder SeedRoles(this ModelBuilder builder)
        {
            builder
                .Entity<IdentityRole>()
                .HasData
                (
                new IdentityRole { Id = "12ff2abd-866c-457e-b725-8dad45ef4885", Name = "user", NormalizedName = "USER" },
                new IdentityRole { Id = "3a590a5a-90de-4886-bc76-69dc9e138b44", Name = "vendor", NormalizedName = "VENDOR" },
                new IdentityRole { Id = "8433688c-a3d9-442b-8e2b-31cc61ca02c2", Name = "admin", NormalizedName = "ADMIN" }
                );
            return builder;
        }
    }
}
