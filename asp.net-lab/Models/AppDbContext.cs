using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace asp.net_lab.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
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
            .HasOne<OrganizationEntity>(c => c.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(c => c.OrganizationId);
        
        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    Name = "WSEI",
                    NIP = "2352434432",
                    REGON = "26930432",
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    Name = "PKP",
                    NIP = "5331123",
                    REGON = "77647345"
                }
            );

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
                    Created = DateTime.Now,
                    OrganizationId = 101
                },
                new ContactEntity()

                {
                    Id = 2,
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    BirthDate = new DateOnly(2000, 12, 12),
                    PhoneNumber = "791234012",
                    Email = "akowalski@gmail.com",
                    Created = DateTime.Now,
                    OrganizationId = 102
                }
            );
        

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(organization => organization.Address)
            .HasData(
                new { OrganizationEntityId = 101, City = "Kraków", Street = "św. Filipa 17" },
                new { OrganizationEntityId = 102, City = "Warszawa", Street = "Dworcowa 9" }
            );
        



       
        //base.OnModelCreating(modelBuilder);
    }
}