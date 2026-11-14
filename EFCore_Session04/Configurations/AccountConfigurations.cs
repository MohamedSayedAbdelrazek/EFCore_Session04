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
    internal class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountNumber);

            builder.Property(a => a.AccountNumber)
                .ValueGeneratedNever();

            builder.Property(a => a.CurrentBalance)
                .HasPrecision(18, 2);

            builder.HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchId);
        }
    }
}
