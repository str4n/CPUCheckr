using CPUCheckr.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPUCheckr.Core.DAL.Configurations;

internal sealed class ProcessorConfiguration : IEntityTypeConfiguration<Processor>
{
    public void Configure(EntityTypeBuilder<Processor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Manufacturer)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));
        
        builder.Property(x => x.Model)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));
        
        builder.Property(x => x.Socket)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));
        
        builder.Property(x => x.ClockRate)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));
        
        builder.Property(x => x.Cores)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));

        builder.Property(x => x.Price)
            .HasConversion(x => x.Value, x => new(x));
    }
}