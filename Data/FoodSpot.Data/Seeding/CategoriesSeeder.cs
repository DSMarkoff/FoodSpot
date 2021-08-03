namespace FoodSpot.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodSpot.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>
            {
                new Category { Name = "Основни ястия", Image = "1.png" },
                new Category { Name = "Супи", Image = "2.png" },
                new Category { Name = "Салати", Image = "3.png" },
                new Category { Name = "Предястия", Image = "4.png" },
                new Category { Name = "Десерти", Image = "5.png" },
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
