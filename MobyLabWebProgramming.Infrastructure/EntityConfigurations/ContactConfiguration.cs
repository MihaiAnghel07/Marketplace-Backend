using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Mobile)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.Address)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.City)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(e => e.User)
           .WithOne(e => e.Contact)
           .HasForeignKey<Contact>(e => e.UserId)
           .IsRequired(false);
    }
}
