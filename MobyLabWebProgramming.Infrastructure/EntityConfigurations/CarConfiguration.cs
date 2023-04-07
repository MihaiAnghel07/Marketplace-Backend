using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Brand)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.Model)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.Year)
            .HasMaxLength(4)
            .IsRequired();
        builder.Property(e => e.Km)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(e => e.Price)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(e => e.User) 
           .WithMany(e => e.Cars) 
           .HasForeignKey(e => e.UserId) 
           .HasPrincipalKey(e => e.Id)
           //.IsRequired()
           .OnDelete(DeleteBehavior.Cascade);
        
    }
}
