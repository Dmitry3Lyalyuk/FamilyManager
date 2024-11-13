﻿using FamilyManager.Application.Common;
using FamilyManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FamilyManager.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext

    {
        public DbSet<User> Users { get; set; }
        public DbSet<Template> Template { get; set; }

        public DbSet<Family> Families { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Template>()
                .HasMany(t => t.Families)
                .WithMany(t => t.Templates);

            builder.Entity<Template>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId);

            builder.Entity<Family>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId);



        }
    }
}
