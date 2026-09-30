using Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.RL;
public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "ADMIN",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id ="2",
                Name = "USER",
                NormalizedName = "USER"
            }
        );

        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "1",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin1234!"),
                SecurityStamp = string.Empty,
            },
            new IdentityUser
            {
                Id = "2",
                UserName = "hg@mail.com",
                NormalizedUserName = "HG@MAIL.COM",
                Email = "hg@mail.com",
                NormalizedEmail = "HG@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Hg1234!"),
                SecurityStamp = string.Empty,
            },
            new IdentityUser
            {
                Id = "3",
                UserName = "sten@mail.com",
                NormalizedUserName = "STEN@MAIL.COM",
                Email = "sten@mail.com",
                NormalizedEmail = "STEN@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Sten1234!"),
                SecurityStamp = string.Empty,
            },
            new IdentityUser
            {
                Id = "4",
                UserName = "kalle@mail.com",
                NormalizedUserName = "KALLE@MAIL.COM",
                Email = "kalle@mail.com",
                NormalizedEmail = "KALLE@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Kalle1234!"),
                SecurityStamp = string.Empty,
            },
            new IdentityUser
            {
                Id = "5",
                UserName = "quinn@mail.com",
                NormalizedUserName = "QUINN@MAIL.COM",
                Email = "quinn@mail.com",
                NormalizedEmail = "QUINN@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Quinn1234!"),
                SecurityStamp = string.Empty,
            },
            new IdentityUser
            {
                Id = "6",
                UserName = "matthijs@mail.com",
                NormalizedUserName = "MATTHIJS@MAIL.COM",
                Email = "matthijs@mail.com",
                NormalizedEmail = "MATTHIJS@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Matthijs1234!"),
                SecurityStamp = string.Empty,
            },
            new IdentityUser
            {
                Id = "7",
                UserName = "baraa@mail.com",
                NormalizedUserName = "BARAA@MAIL.COM",
                Email = "baraa@mail.com",
                NormalizedEmail = "BARAA@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Baraa1234!"),
                SecurityStamp = string.Empty,
            }
        );
    }
}