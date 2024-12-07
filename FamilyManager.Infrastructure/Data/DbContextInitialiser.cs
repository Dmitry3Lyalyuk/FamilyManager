using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace FamilyManager.Infrastructure.Data
{
    public class DbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        public DbContextInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
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

                await _context.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }


    }

}
