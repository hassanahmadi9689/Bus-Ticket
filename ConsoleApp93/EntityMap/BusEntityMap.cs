using ConsoleApp93.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp93.EntityMap;

public class BusEntityMap : IEntityTypeConfiguration<Bus>
{
    public void Configure(EntityTypeBuilder<Bus> builder)
    {
        
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd();
        builder.Property(_ => _.Name).IsRequired();
        builder.Property(_ => _.Capacity).IsRequired();
        builder.HasMany(_ => _.Trips).WithOne(_ => _.Bus)
            .HasForeignKey(_ => _.BusId).OnDelete(DeleteBehavior.NoAction);
        
    }
}