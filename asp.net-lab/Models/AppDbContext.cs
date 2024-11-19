using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace asp.net_lab.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
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
        base.OnModelCreating(modelBuilder);

        string USER_ID = Guid.NewGuid().ToString();
        string ADMIN_ID = Guid.NewGuid().ToString();

        string USER_ROLE_ID = Guid.NewGuid().ToString();
        string ADMIN_ROLE_ID = Guid.NewGuid().ToString();


        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole()
                {
                    Id = USER_ROLE_ID,
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = USER_ROLE_ID
                },
                new IdentityRole()
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = ADMIN_ROLE_ID
                }
            );

        var user = new IdentityUser()
        {
            Id = USER_ID,
            Email = "adam@wsei.edu.pl",
            NormalizedEmail = "ADAM@WSEI.EDU.PL",
            UserName = "Adam",
            NormalizedUserName = "ADAM",
            EmailConfirmed = true
        };
        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            Email = "admin@wsei.edu.pl",
            NormalizedEmail = "ADMIN@WSEI.EDU.PL",
            UserName = "ADMIN",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true
        };
        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = hasher.HashPassword(user, "1234!");
        admin.PasswordHash = hasher.HashPassword(admin, "qwerty123!");

        modelBuilder.Entity<IdentityUser>()
            .HasData(user, admin);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                });

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