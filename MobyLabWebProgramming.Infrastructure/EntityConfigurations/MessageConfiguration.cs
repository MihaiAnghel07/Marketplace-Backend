﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.CarId)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(e => e.Text)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(e => e.User)
           .WithMany(e => e.Messages)
           .HasForeignKey(e => e.UserId)
           .HasPrincipalKey(e => e.Id)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.Cascade);

    }
}
