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
    internal class ManagerConfigurations : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FullName).HasMaxLength(150);
            builder.Property(m => m.Email).HasMaxLength(100);

            builder.HasData(
                new Manager
                {
                    Id = 1,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed@nationalbank.com",
                    PhoneNumber = "01012345678",
                    HireDate = new DateOnly(2020, 1, 15)
                },
                new Manager
                {
                    Id = 2,
                    FullName = "Mohamed Ali",
                    Email = "mohamed@nationalbank.com",
                    PhoneNumber = "01112345678",
                    HireDate = new DateOnly(2021, 6, 10)
                }
            );
        }
    }
}
