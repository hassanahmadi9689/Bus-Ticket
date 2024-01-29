using ConsoleApp93.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp93.EntityMap;

public class EFDataContext : DbContext
{
    public DbSet<Bus> Buses { get; set; }
    public DbSet<VIPBus> VIPBuses { get; set; }
    public DbSet<NormalBus> NormalBuses { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<City>  Cities { get; set; }
    public DbSet<TakeTicket> TakeTickets { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=project8;\n" +
                                    "Trusted_Connection=true;TrustServerCertificate=yes");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);


        modelBuilder.Entity<Bus>().HasDiscriminator<BusType>("Type")
            .HasValue<NormalBus>(BusType.Normal)
            .HasValue<VIPBus>(BusType.VIP);
    }
    
}