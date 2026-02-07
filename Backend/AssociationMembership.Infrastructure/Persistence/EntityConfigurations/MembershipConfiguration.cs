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
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Memberships");

            
            builder.HasKey(x => x.Id);

            
            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.MotivationText)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(x => x.IdentityNumber)
                   .IsRequired()
                   .HasMaxLength(11);


            builder.Property(x => x.BirthDate)
                   .IsRequired();


            builder.Property(x => x.BirthPlace)
                   .HasMaxLength(100);


            builder.Property(x => x.MobilePhone)
                   .IsRequired()
                   .HasMaxLength(20);



            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Email);



            builder.Property(x => x.MotherName)
                   .HasMaxLength(50);

            builder.Property(x => x.FatherName)
                   .HasMaxLength(50);

            builder.Property(x => x.BloodType)
                   .HasMaxLength(5);
            
            builder.Property(x => x.EducationLevel)
                   .HasMaxLength(100);

            builder.Property(x => x.Profession)
                   .HasMaxLength(100);

            builder.Property(x => x.WorkplaceName)
                   .HasMaxLength(150);

            builder.Property(x => x.WorkplacePosition)
                   .HasMaxLength(150);

            builder.Property(x => x.WorkPhone)
                   .HasMaxLength(20);

            builder.Property(x => x.ResidenceAddress)
                   .HasMaxLength(1000);

            builder.Property(x => x.WorkplaceAddress)
                   .HasMaxLength(1000);



            
            builder.Property(x => x.KvkkAccepted)
                   .IsRequired();

            builder.Property(x => x.ExplicitConsentAccepted)
                   .IsRequired();

            builder.Property(x => x.ConsentAcceptedDate);


            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);



            builder.HasOne(m => m.TenantGroup)
                   .WithMany(g => g.Memberships)
                   .HasForeignKey(m => m.TenantGroupId)
                   .OnDelete(DeleteBehavior.Restrict);



            builder.HasOne(m => m.Tracking)
                   .WithOne(t => t.Membership)
                   .HasForeignKey<MembershipTracking>(t => t.MembershipId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(m => new { m.TenantGroupId, m.IdentityNumber })
                   .IsUnique()
                   .HasFilter("\"IsDeleted\" = false");

        }
    }
}
