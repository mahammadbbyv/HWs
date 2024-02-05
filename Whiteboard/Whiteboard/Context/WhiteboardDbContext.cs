using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.Model;

namespace Whiteboard.Context
{
    public class WhiteboardDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SketchModel> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("LocalConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<UserModel>();
            var pictures = modelBuilder.Entity<SketchModel>();

            users.HasKey(x => x.Id);
            pictures.HasKey(x => x.Id);

            users.Property(x => x.Username).IsRequired();
            users.Property(x => x.Password).IsRequired();
            users.Property(x => x.Email).IsRequired();

            pictures.Property(x => x.ProjectName).IsRequired();
            pictures.Property(x => x.ProjectLink).IsRequired();
            pictures.Property(x => x.DateCreated).IsRequired();
        }
    }
}
