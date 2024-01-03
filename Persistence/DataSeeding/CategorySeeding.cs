using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataSeeding
{
    public static class CategorySeeding
    {
        public static ModelBuilder SeedCategories(this ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "قاعة" },
                new Category { Id = 2, Name = "قاعة اسلامي" },
                new Category { Id = 3, Name = "نادي" },
                new Category { Id = 4, Name = "مصور" },
                new Category { Id = 5, Name = "مطعم" },
                new Category { Id = 6, Name = "صالون تجميل" },
                new Category { Id = 7, Name = "كوافير للرجال" },
                new Category { Id = 8, Name = "كوافير للسيدات" }
            );
            return builder;
        }
    }
}
