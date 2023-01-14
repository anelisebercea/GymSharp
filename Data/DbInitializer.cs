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

                if (context.Users.Any())
                {
                    return;
                }
                context.Users.AddRange(
                new User
                {
                    Name = "Amanda Perez",
                    Email = "amandaperez@gymsharp.com",
                    BirthDate = DateTime.Parse("1995-05-18"),
                    Gender = "Female",
                    Height = 175,
                }  
                
                );



                context.SaveChanges();
            }
        }

    }
}
