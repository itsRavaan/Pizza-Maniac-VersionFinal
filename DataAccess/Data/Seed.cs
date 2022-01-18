using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.DataAccess.Data
{
    public class Seed
    {
        public static async Task SeedPizzas(ApplicationDbContext context)
        {
            
            if (await context.Pizza.AnyAsync()) return;

            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + @"\Data/BookSeedData.json";

            var bookData = await System.IO.File.ReadAllTextAsync(filePath);

            //No need to use Newtonsoft JSON since .net core 3.1
            //Deserialize the pizza JSON file content
            var pizzas = JsonSerializer.Deserialize<List<Pizza>>(bookData);

          
            foreach (var pizza in pizzas)
            {

                //Add to memory in EF Core
                context.Pizza.Add(pizza);
            }

            //Persist changes To DB asynchronously
            await context.SaveChangesAsync();
        }
    }
}
