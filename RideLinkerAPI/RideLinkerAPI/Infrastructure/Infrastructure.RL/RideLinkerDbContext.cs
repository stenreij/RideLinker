using Microsoft.EntityFrameworkCore;
using Core.Domain;

namespace Infrastructure.RL
{
    public class RideLinkerDbContext : DbContext
    {

        public DbSet<Car> Cars { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }

        public RideLinkerDbContext(DbContextOptions<RideLinkerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.UserId, r.TripId })
                .IsUnique();

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Breda", Address = "Konijnenberg 24" },
                new Location { Id = 2, Name = "Amsterdam", Address = "Nachtwachtlaan 20" },
                new Location { Id = 3, Name = "Utrecht", Address = "Vliegend Hertlaan 39" },
                new Location { Id = 4, Name = "Arnhem", Address = "Velperweg 27" },
                new Location { Id = 5, Name = "Enschede", Address = "Brouwerijstraat 1" },
                new Location { Id = 6, Name = "Maastricht", Address = "Vrijthof 23" }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Volkswagen", Model = "ID.3", Image = "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", Available = true, LocationId = 1 },
                new Car { Id = 2, Brand = "Volkswagen", Model = "ID.3", Image = "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", Available = true, LocationId = 1 },
                new Car { Id = 3, Brand = "Volkswagen", Model = "ID.3", Image = "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", Available = true, LocationId = 1 },
                new Car { Id = 4, Brand = "Volkswagen", Model = "ID.3", Image = "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", Available = true, LocationId = 1 },
                new Car { Id = 5, Brand = "Audi", Model = "E-tron", Image = "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", Available = true, LocationId = 1 },
                new Car { Id = 6, Brand = "Audi", Model = "E-tron", Image = "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", Available = true, LocationId = 1 },
                new Car { Id = 7, Brand = "Audi", Model = "E-tron", Image = "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", Available = true, LocationId = 1 },
                new Car { Id = 8, Brand = "Audi", Model = "E-tron", Image = "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", Available = true, LocationId = 2 }
            );

            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@mail.com", BirthDate = new DateTime(2000, 01, 01, 0, 0, 0), UserRole = Role.ADMIN },
                new User { Id = 2, Name = "Hans Gerard", Email = "hg@mail.com", BirthDate = new DateTime(2000, 01, 01, 0, 0, 0), UserRole = Role.EMPLOYEE },
                new User { Id = 3, Name = "Sten", Email = "sten@mail.com", BirthDate = new DateTime(2000, 10, 28, 0, 0, 0), UserRole = Role.EMPLOYEE },
                new User { Id = 4, Name = "Kalle", Email = "kalle@mail.com", BirthDate = new DateTime(2001, 02, 01, 0, 0, 0), UserRole = Role.ADMIN },
                new User { Id = 5, Name = "Quinn", Email = "quinn@mail.com", BirthDate = new DateTime(2001, 02, 01, 0, 0, 0), UserRole = Role.ADMIN },
                new User { Id = 6, Name = "Matthijs", Email = "matthijs@mail.com", BirthDate = new DateTime(2001, 02, 01, 0, 0, 0), UserRole = Role.EMPLOYEE },
                new User { Id = 7, Name = "Baraa", Email = "baraa@mail.com", BirthDate = new DateTime(2001, 02, 01, 0, 0, 0), UserRole = Role.EMPLOYEE }
            );

            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 1, StartTime = new DateTime(2024, 01, 22, 12, 0, 0), EndTime = new DateTime(2024, 01, 22, 14, 0, 0), DepartureId = 1, DestinationId = 2, CarId = 1, DriverId = 2 },
                new Trip { Id = 2, StartTime = new DateTime(2024, 01, 23, 12, 0, 0), EndTime = new DateTime(2024, 01, 23, 13, 0, 0), DepartureId = 2, DestinationId = 1, CarId = 1, DriverId = 2 },
                new Trip { Id = 3, StartTime = new DateTime(2024, 01, 23, 15, 0, 0), EndTime = new DateTime(2024, 01, 23, 16, 0, 0), DepartureId = 1, DestinationId = 5, CarId = 2, DriverId = 3 },
                new Trip { Id = 4, StartTime = new DateTime(2024, 01, 27, 12, 0, 0), EndTime = new DateTime(2024, 01, 27, 14, 0, 0), DepartureId = 5, DestinationId = 1, CarId = 2, DriverId = 3 },
                new Trip { Id = 5, StartTime = new DateTime(2024, 01, 27, 12, 0, 0), EndTime = new DateTime(2024, 01, 27, 14, 0, 0), DepartureId = 1, DestinationId = 2, CarId = 3, DriverId = 1 },
                new Trip { Id = 6, StartTime = new DateTime(2024, 01, 27, 17, 0, 0), EndTime = new DateTime(2024, 01, 27, 19, 0, 0), DepartureId = 1, DestinationId = 6, CarId = 4, DriverId = 4 },
                new Trip { Id = 7, StartTime = new DateTime(2024, 02, 01, 17, 0, 0), EndTime = new DateTime(2024, 02, 01, 19, 0, 0), DepartureId = 1, DestinationId = 6, CarId = 4, DriverId = 4 },
                new Trip { Id = 8, StartTime = new DateTime(2024, 02, 01, 17, 0, 0), EndTime = new DateTime(2024, 01, 02, 19, 0, 0), DepartureId = 1, DestinationId = 6, CarId = 5, DriverId = 5 },
                new Trip { Id = 9, StartTime = new DateTime(2024, 02, 02, 17, 0, 0), EndTime = new DateTime(2024, 02, 02, 19, 0, 0), DepartureId = 6, DestinationId = 4, CarId = 5, DriverId = 5 },
                new Trip { Id = 10, StartTime = new DateTime(2024, 02, 02, 17, 0, 0), EndTime = new DateTime(2024, 02, 02, 19, 0, 0), DepartureId = 4, DestinationId = 6, CarId = 6, DriverId = 6 },
                new Trip { Id = 11, StartTime = new DateTime(2024, 02, 02, 17, 0, 0), EndTime = new DateTime(2024, 02, 02, 19, 0, 0), DepartureId = 6, DestinationId = 1, CarId = 6, DriverId = 6 },
                new Trip { Id = 12, StartTime = new DateTime(2024, 02, 03, 17, 0, 0), EndTime = new DateTime(2024, 02, 03, 19, 0, 0), DepartureId = 2, DestinationId = 5, CarId = 8, DriverId = 7 },
                new Trip { Id = 13, StartTime = new DateTime(2024, 02, 03, 17, 0, 0), EndTime = new DateTime(2024, 02, 03, 19, 0, 0), DepartureId = 5, DestinationId = 2, CarId = 8, DriverId = 7 }

                );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, UserId = 2, TripId = 1 },
                new Reservation { Id = 2, UserId = 2, TripId = 2 },
                new Reservation { Id = 3, UserId = 3, TripId = 3 },
                new Reservation { Id = 4, UserId = 3, TripId = 4 },
                new Reservation { Id = 5, UserId = 1, TripId = 5 },
                new Reservation { Id = 6, UserId = 4, TripId = 6 },
                new Reservation { Id = 7, UserId = 4, TripId = 7 },
                new Reservation { Id = 8, UserId = 5, TripId = 8 },
                new Reservation { Id = 9, UserId = 5, TripId = 9 },
                new Reservation { Id = 10, UserId = 6, TripId = 10 },
                new Reservation { Id = 11, UserId = 6, TripId = 11 },
                new Reservation { Id = 12, UserId = 7, TripId = 12 },
                new Reservation { Id = 13, UserId = 7, TripId = 13 }
            );


            base.OnModelCreating(modelBuilder);
        }

    }
}
