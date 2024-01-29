using System.Security.Cryptography;
using ConsoleApp93.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp93.EntityMap;

public class TripEntityMap : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(_ => _.OriginCity).WithMany(_ => _.TripOrigins)
            .HasForeignKey(_ => _.OriginId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(_ => _.DestinationCity).WithMany(_ => _.TripDestinations)
            .HasForeignKey(_ => _.DestinationId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(_ => _.TakeTickets).WithOne(_ => _.Trip)
            .HasForeignKey(
                _ => _.TripId);
    }
}