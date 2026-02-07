using AssociationMembership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Infrastructure.Persistence.EntityConfigurations
{
    public class TenantGroupConfiguration : IEntityTypeConfiguration<TenantGroup>
    {
        public void Configure(EntityTypeBuilder<TenantGroup> builder)
        {
            builder.ToTable("TenantGroups");


            builder.HasKey(g => g.Id);


            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(g => g.Description)
                   .HasMaxLength(500);

            builder.HasIndex(x => x.Slug).IsUnique();
            builder.Property(x => x.Slug)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasOne(g => g.Tenant)
                   .WithMany(t => t.TenantGroups)
                   .HasForeignKey(g => g.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.Memberships)
                   .WithOne(m => m.TenantGroup)
                   .HasForeignKey(m => m.TenantGroupId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Users)
                   .WithOne(x => x.TenantGroup)
                   .HasForeignKey(x => x.TenantGroupId)
                   .OnDelete(DeleteBehavior.Restrict);



            builder.HasIndex(g => new { g.TenantId, g.Name })
                   .IsUnique();
        }
    }
}
