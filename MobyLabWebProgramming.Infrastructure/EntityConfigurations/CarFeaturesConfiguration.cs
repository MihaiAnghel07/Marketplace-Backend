
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;


public class CarFeaturesConfiguration : IEntityTypeConfiguration<CarFeatures>
{
    public void Configure(EntityTypeBuilder<CarFeatures> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        //builder.HasKey(x => x.Id);
        builder.HasKey(x => new { x.CarId, x.FeatureId });
        
        builder.HasOne(e => e.Car)
            .WithMany(e => e.CarFeatures)
            .HasForeignKey(e => e.CarId)
            .IsRequired(false);

        builder.HasOne(e => e.Feature)
            .WithMany(e => e.CarFeatures)
            .HasForeignKey(e => e.FeatureId)
            .IsRequired(false);
        
    }
}