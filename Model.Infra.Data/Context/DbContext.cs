using Domain.Entities;
using Infra.Data.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Destino> Destinos { get; set; }

        public DbSet<Dispositivo> Dispositivos { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolDBContext, EF6Console.Migrations.Configuration>());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(b => b.Dispositivos)
                .WithOne();

            modelBuilder.Entity<Destino>()
              .Property(b => b.Alvos)
              .HasJsonConversion();

            modelBuilder.Entity<Dispositivo>()
                .HasOne(b => b.Destino)
                .WithOne()
                .HasForeignKey<Destino>(b => b.DestinoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
