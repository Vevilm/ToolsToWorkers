using Microsoft.AspNetCore.Identity;
using ToolsToWorkers.Data;
using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        //new User("Иванов", "Иван", "Иванович", "Кладовщик", "Работает", "LoginIII", PasswordHasher.Generate("11111")),
                        //new User("Сергеев", "Сергей", "Сергеевич", "Администратор", "Работает", "LoginSSS", PasswordHasher.Generate("22222")),
                        //new User("Петров", "Пётр", "Петрович", "Рабочий", "Работает", "LoginPPP", PasswordHasher.Generate("33333"))
                    });
                    context.SaveChanges();
                }
                if (!context.Storages.Any())
                {
                    context.Storages.AddRange(new List<Storage>()
                    {
                        new Storage("Склад №1", "Склад", "Работает"),
                        new Storage("Цех №1", "Цех", "Работает"),
                        new Storage("Шкаф №1", "Шкаф", "Работает")
                    });
                    context.SaveChanges();
                }
                if (!context.Tools.Any())
                {
                    context.Tools.AddRange(new List<Tool>()
                    {
                        new Tool(1, "10321201", "Доступен"),
                        new Tool(3, "10111201", "Списан"),
                        new Tool(2, "10323551", "Доступен"),
                    });
                    context.SaveChanges();
                }
                if (!context.ToolRequests.Any())
                {
                    context.ToolRequests.AddRange(new List<ToolRequest>()
                    {
                        new ToolRequest(1, 1, "Запрошен", DateTime.Now)
                    });
                    context.SaveChanges();
                }
                if (!context.Articles.Any())
                {
                    context.Articles.AddRange(new List<Article>()
                    {
                        new Article("10321201", "Молоток с деревянной ручкой", "Длинное описание молотка...", 0.8),
                        new Article("10111201", "Сварочный аппарат", "Длинное описание сварочного аппарата...", 10),
                        new Article("10323551", "Кувалда", "Кувалда - это кувалда. Она не нуждается в представлении", 2)
                    });
                    context.SaveChanges();
                }
            }
        }

        //public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        //Roles
        //        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //        if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //        //Users
        //        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        //        string adminUserEmail = "teddysmithdeveloper@gmail.com";

        //        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        //        if (adminUser == null)
        //        {
        //            var newAdminUser = new AppUser()
        //            {
        //                UserName = "teddysmithdev",
        //                Email = adminUserEmail,
        //                EmailConfirmed = true,
        //                Address = new Address()
        //                {
        //                    Street = "123 Main St",
        //                    City = "Charlotte",
        //                    State = "NC"
        //                }
        //            };
        //            await userManager.CreateAsync(newAdminUser, "Coding@1234?");
        //            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
        //        }

        //        string appUserEmail = "user@etickets.com";

        //        var appUser = await userManager.FindByEmailAsync(appUserEmail);
        //        if (appUser == null)
        //        {
        //            var newAppUser = new AppUser()
        //            {
        //                UserName = "app-user",
        //                Email = appUserEmail,
        //                EmailConfirmed = true,
        //                Address = new Address()
        //                {
        //                    Street = "123 Main St",
        //                    City = "Charlotte",
        //                    State = "NC"
        //                }
        //            };
        //            await userManager.CreateAsync(newAppUser, "Coding@1234?");
        //            await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
        //        }
        //    }
        //}
    }
}