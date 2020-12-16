using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser{
                    DisplayName = "Anand",
                    Email = "anand@test.com",
                    UserName= "anand@test.com",
                    Address = new Address{
                        FirstName = "Anand",
                        LastName ="Mishra",
                        Street = "Testing Street",
                        City = "Lucknow",
                        State= "Uttar Pradesh",
                        PinCode = "23001"

                    }
                };
                await userManager.CreateAsync(user ,"P@ssw0rd");
            }
        }
    }
}