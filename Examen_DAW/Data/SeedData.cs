using Microsoft.EntityFrameworkCore;

namespace Examen_DAW.Data
{
    public class SeedData
    {
        public static void GenerateData(IServiceProvider _ServiceProvider)
        {
            ApplicationDbContext _MyDataBase = new ApplicationDbContext(_ServiceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        }

    }
}
