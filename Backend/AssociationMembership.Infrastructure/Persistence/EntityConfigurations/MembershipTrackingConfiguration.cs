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
    public class MembershipTrackingConfiguration : IEntityTypeConfiguration<MembershipTracking>
    {
        public void Configure(EntityTypeBuilder<MembershipTracking> builder)
        {
            builder.ToTable("MembershipTrackings");


            builder.HasKey(x => x.Id);


            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(x => x.PaymentAmount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(500);


            builder.Property(x => x.ApprovedDate)
                   .IsRequired(false);


            builder.Property(x => x.PaymentReceivedDate)
                     .IsRequired(false);





            builder.HasOne(x => x.Membership)
                   .WithOne(m => m.Tracking)
                   .HasForeignKey<MembershipTracking>(x => x.MembershipId)
                   .OnDelete(DeleteBehavior.Cascade);



            builder.HasIndex(x => x.MembershipId)
                   .IsUnique();

            builder.HasIndex(x => x.Status);
            


            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

        }
    }
}
