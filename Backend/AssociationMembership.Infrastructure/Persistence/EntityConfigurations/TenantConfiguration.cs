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
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {

            builder.ToTable("Tenants");


            builder.HasKey(x => x.Id);


            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200);


            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasIndex(x => x.Code)
                   .IsUnique();

            builder.Property(t => t.Description)
                   .HasMaxLength(500);


            builder.Property(t => t.LogoUrl)
                   .HasMaxLength(500);


            builder.HasIndex(x => x.Slug).IsUnique();
            builder.Property(x => x.Slug)
                   .HasMaxLength(100)
                   .IsRequired();


            builder.Property(x => x.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedDate)
                   .IsRequired();


            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);



            builder.HasMany(t => t.TenantGroups)
               .WithOne(g => g.Tenant)
               .HasForeignKey(g => g.TenantId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
