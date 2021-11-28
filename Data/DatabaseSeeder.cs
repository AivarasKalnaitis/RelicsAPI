using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Auth.Model;
using RelicsAPI.Data.DTOs.Auth;
using RelicsAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await SeedUsers();
            await SeedCategories();
            await SeedRelics();
        }

        private async Task SeedUsers()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var newAdminUser = new User
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);

            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "Admin123!");

                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, UserRoles.All);
                }
            }

            var newUser1 = new User
            {
                UserName = "user1",
                Email = "user1@gmail.com",
            };

            var existingUser = await _userManager.FindByNameAsync(newUser1.UserName);

            if (existingUser == null)
            {
                var createUserResult = await _userManager.CreateAsync(newUser1, "User1!");

                if (createUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser1, UserRoles.Customer);
                }
            }

            var newUser2 = new User
            {
                UserName = "user2",
                Email = "user2@gmail.com",
            };

            existingUser = await _userManager.FindByNameAsync(newUser2.UserName);

            if (existingUser == null)
            {
                var createUserResult = await _userManager.CreateAsync(newUser2, "User2!");

                if (createUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser2, UserRoles.Customer);
                }
            }
        }

        private static async Task SeedCategories()
        {
            using var context = new RelicsContext();

            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(new List<Category>
                {
                    new Category
                    {
                        Name = "Persian",
                    },
                    new Category
                    {
                        Name = "Japanese",
                    }
                });
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedRelics()
        {
            using var context = new RelicsContext();

            if (!context.Relics.Any())
            {
                await context.Relics.AddRangeAsync(new List<Relic>
                {
                    new Relic
                    {
                        Name = "French coin",
                        Materials = new string[]{"steel"},
                        Creator = "Clovis Gauthier",
                        Price = 50,
                        Period = "French Revolution",
                        YearMade = "1793",
                        Category = new Category
                        {
                            Name = "French",
                        }
                    },
                    new Relic
                    {
                        Name = "Miniature of egyptian Pyramid",
                        Materials = new string[]{"sand", "gold"},
                        Creator = "Sch-ea Horu",
                        Price = 150,
                        Period = "Ancient Egypt",
                        Category = new Category
                        {
                            Name = "Egyptian",
                        }
                    },
                    new Relic
                    { 
                        Name = "Camel sadle of Waqeea Saad",
                        Materials = new string[]{"leather", "iron"},
                        Creator = "Aafreen Salik",
                        Price = 2000,
                        Period = "Ancient Arabia",
                        YearMade = "618",
                        Category = new Category
                        {
                            Name = "Arabian",
                        }
                    },
                    new Relic
                    {
                        Name = "Samurai sword",
                        Materials = new string[]{"obsidian", "steel"},
                        Creator = "Kitagawa Hitomi",
                        Price = 12500,
                        Period = "Feudal Japan",
                        YearMade = "1249",
                        Category = new Category
                        {
                            Name = "Japanese",
                        }
                    }
                    //new Relic
                    //{
                    //    Name = "Samurai helmet",
                    //    Materials = new string[]{"steel"},
                    //    Creator = "Machida Momoko",
                    //    Price = 9000,
                    //    Period = "Feudal Japan",
                    //    YearMade = "1349",
                    //    Category = new Category
                    //    {
                    //        Name = "Japanese",
                    //    }
                    //}
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
