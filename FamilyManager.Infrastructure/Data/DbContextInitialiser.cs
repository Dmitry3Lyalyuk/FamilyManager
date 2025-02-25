﻿using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace FamilyManager.Infrastructure.Data
{
    public class DbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<DbContextInitialiser> _logger;

        public DbContextInitialiser(ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<DbContextInitialiser> logger
            )
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while applying mirgation");
                throw new Exception("An error occured while applying mirgation");
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                foreach (var roleName in Enum.GetNames(typeof(ApplicationRole)))
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
                    }
                }
                var adminEmail = "NoN@admin.com";
                var adminUser = await _userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new User()
                    {
                        UserName = "admin123",
                        Email = adminEmail,
                        Status = Status.Individual,
                        Country = Country.Turkey

                    };
                    var result = await _userManager.CreateAsync(adminUser, "Admin123124!");

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, ApplicationRole.Admin.ToString());
                    }
                    else
                    {
                        _logger.LogError("Admin creation failed");
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while seeding the database");
                throw new Exception ("An error occured while seeding the database");
            }
        }
    }
}
