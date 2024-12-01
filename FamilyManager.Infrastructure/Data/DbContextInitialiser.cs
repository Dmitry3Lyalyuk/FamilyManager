using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using FamilyManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace FamilyManager.Infrastructure.Data
{
    public class DbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbContextInitialiser> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbContextInitialiser(ApplicationDbContext context,
            ILogger<DbContextInitialiser> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
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
                _logger.LogError("An error occured while applying migrations.");
                throw;
            }
        }
        public async Task SeedAsync()
        {
            try
            {
                var adminId = Guid.NewGuid();

                if (!_context.Users.Any())
                {

                    _context.Users.AddRange(
                        new User
                        {
                            Id = Guid.NewGuid(),
                            UserName = "admin228",
                            Status = Status.Individual,
                            Role = "admin",
                            Country = Country.Russia,
                            Email = "admin@gmail.com",

                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            UserName = "Tolik",
                            Status = Status.Individual,
                            Role = "explorer",
                            Country = Country.Turkey,
                            Email = "tolik@gmail.com",

                        },
                         new User
                         {
                             Id = Guid.NewGuid(),
                             UserName = "Dima",
                             Status = Status.Individual,
                             Role = "explorer",
                             Country = Country.England,
                             Email = "dimon@gmail.com",

                         },
                          new User
                          {
                              Id = Guid.NewGuid(),
                              UserName = "BlackDuck",
                              Status = Status.Company,
                              Role = "explorer",
                              Country = Country.England,
                              Email = "ru@gmail.com",

                          }
                        );
                }

                //if (!_context.Families.Any())
                //{
                //    _context.Families.AddRange(
                //        new Family()
                //        {
                //            Id = Guid.NewGuid(),
                //            Category = Category.Wall,
                //            Brand = null,
                //            Name = "Base_wall",
                //            CreatedAt = DateTime.Now,
                //            CreatedBy = adminId,
                //            LastModifiedAt = DateTime.Now,
                //            LastModifiedBy = adminId
                //        },
                //        new Family()
                //        {
                //            Id = Guid.NewGuid(),
                //            Category = Category.Wall,
                //            Brand = null,
                //            Name = "Lightins bright",
                //            CreatedAt = DateTime.Now,
                //            CreatedBy = adminId,
                //            LastModifiedAt = DateTime.Now,
                //            LastModifiedBy = adminId
                //        });
                //}

                await _context.SaveChangesAsync();

            }
            catch
            {
                _logger.LogError("An error occured while seeding the database.");
                throw;
            }

        }
        public async Task TrySeedAsync()
        {
            var roles = Enum.GetNames(typeof(Roles));
            foreach (var role in roles)
            {
                if (_roleManager.Roles.All(r => r.Name != role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var admin = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            };
            if (_userManager.Users.All(u => u.UserName != admin.UserName))
            {
                await _userManager.CreateAsync(admin, "Admin111");
                await _userManager.AddToRolesAsync(admin, [Roles.Administrator.ToString()]);
            }
        }
    }

}
