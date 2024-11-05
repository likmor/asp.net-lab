using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace asp.net_lab.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    BirthDate = new DateOnly(2000, 12, 12),
                    PhoneNumber = "791234012",
                    Email = "akowalski@gmail.com",
                    Created = DateTime.Now
                },
                new ContactEntity()

                {
                    Id = 2,
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    BirthDate = new DateOnly(2000, 12, 12),
                    PhoneNumber = "791234012",
                    Email = "akowalski@gmail.com",
                    Created = DateTime.Now
                }
            );
        //base.OnModelCreating(modelBuilder);
    }
}