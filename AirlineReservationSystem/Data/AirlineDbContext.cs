// Data/AirlineDbContext.cs
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

public class AirlineDbContext : DbContext
{
    public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<City> Cities { get; set; }
}
