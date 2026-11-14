using EFCore_Session04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Configurations
{
    internal class BranchConfigurations : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {

            builder.HasKey(b => b.Code);

            builder.HasOne(b => b.Manager)
                .WithOne(m => m.Branch)
                .HasForeignKey<Branch>(b => b.ManagerId);

            builder.Property(b => b.Name)
                .HasMaxLength(100);

            builder.HasData(
                new Branch
                {
                    Code = 1,
                    Name = "Cairo Main Branch",
                    Address = "Cairo, Egypt",
                    PhoneNumber = "0221234567",
                    ManagerId = 1
                },
                new Branch
                {
                    Code = 2,
                    Name = "Alexandria Branch",
                    Address = "Alexandria, Egypt",
                    PhoneNumber = "0331234567",
                    ManagerId = 2
                }
            );
        }
    }
}
