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
    internal class AccountCustomerConfigurations : IEntityTypeConfiguration<AccountCustomer>
    {
        public void Configure(EntityTypeBuilder<AccountCustomer> builder)
        {
            builder.HasKey(nameof(AccountCustomer.AccountId), nameof(AccountCustomer.CustomerId));

            //builder.HasKey(ca => new
            //{
            //    ca.AccountId,
            //    ca.CustomerId
            //});
        }
    }
}
