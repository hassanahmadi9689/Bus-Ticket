using ConsoleApp93.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp93.EntityMap;

public class VIPBusEntityMap : IEntityTypeConfiguration<VIPBus>
{
    public void Configure(EntityTypeBuilder<VIPBus> builder)
    {
       // builder.ToTable("Buses");
    }
}