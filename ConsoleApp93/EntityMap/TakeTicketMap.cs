using ConsoleApp93.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp93.EntityMap;

public class TakeTicketMap : IEntityTypeConfiguration<TakeTicket>
{
    public void Configure(EntityTypeBuilder<TakeTicket> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd();
        
    }
}