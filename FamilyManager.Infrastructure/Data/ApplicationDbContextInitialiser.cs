﻿using FamilyManager.Domain.Entities;
using FamilyManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace FamilyManager.Infrastructure.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbContextInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex) { }
        }
        public async Task SeedAsync()
        {
            try
            {
                if (_context.Users.Any())
                {
                    var adminId = new Guid();

                    _context.Users.AddRange(
                        new User
                        {
                            Id = adminId,
                            UserName = "admin",
                            Status = Status.Individual,
                            Role = "admin",
                            Country = Country.Russia,
                            Email = "admin@gmail.com",
                            CreatedAt = DateTime.Now,
                            CreatedBy = null,
                            LastModifiedAt = DateTime.Now,
                            LastModifiedBy = null
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            UserName = "Tolik",
                            Status = Status.Individual,
                            Role = "explorer",
                            Country = Country.Turkey,
                            Email = "tolik@gmail.com",
                            CreatedAt = DateTime.Now,
                            CreatedBy = null,
                            LastModifiedAt = DateTime.Now,
                            LastModifiedBy = null
                        },
                         new User
                         {
                             Id = Guid.NewGuid(),
                             UserName = "Dima",
                             Status = Status.Individual,
                             Role = "explorer",
                             Country = Country.England,
                             Email = "dimon@gmail.com",
                             CreatedAt = DateTime.Now,
                             CreatedBy = null,
                             LastModifiedAt = DateTime.Now,
                             LastModifiedBy = null
                         },
                          new User
                          {
                              Id = Guid.NewGuid(),
                              UserName = "BlackDuck",
                              Status = Status.Company,
                              Role = "explorer",
                              Country = Country.England,
                              Email = "ru@gmail.com",
                              CreatedAt = DateTime.Now,
                              CreatedBy = null,
                              LastModifiedAt = DateTime.Now,
                              LastModifiedBy = null
                          }
                        );
                }

                if (_context.Families.Any())
                {
                    _context.Families.AddRange(
                        new Family()
                        {
                            Id = Guid.NewGuid(),
                            Category = Category.Wall,
                            Brand = null,
                            Name = "Base_wall",
                            CreatedAt = DateTime.Now,
                            CreatedBy = null,
                            LastModifiedAt = DateTime.Now,
                            LastModifiedBy = null
                        },
                        new Family()
                        {
                            Id = Guid.NewGuid(),
                            Category = Category.Wall,
                            Brand = null,
                            Name = "Lightins bright",
                            CreatedAt = DateTime.Now,
                            CreatedBy = null,
                            LastModifiedAt = DateTime.Now,
                            LastModifiedBy = null
                        }); ; ;
                }

                await _context.SaveChangesAsync();

            }
            catch
            {

            }
        }


    }

}
