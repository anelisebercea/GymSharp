using Microsoft.EntityFrameworkCore;
using GymSharp.Models;


namespace GymSharp.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GymContext(serviceProvider.GetRequiredService
            <DbContextOptions<GymContext>>()))
            {
                


               

                context.SaveChanges();
            }
        }

    }
}
