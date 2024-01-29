using ConsoleApp93.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp93.EntityMap;

public class NormalBusEntityMap : IEntityTypeConfiguration<NormalBus>
{
    public void Configure(EntityTypeBuilder<NormalBus> builder)
    {
       // builder.ToTable("Buses");
    }
}