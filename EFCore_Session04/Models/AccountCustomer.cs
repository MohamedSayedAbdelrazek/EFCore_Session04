using EFCore_Session04.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Models
{
    [PrimaryKey(nameof(AccountId),nameof(CustomerId))]
    internal class AccountCustomer
    {
        public int AccountId;
        public Account Account { get; set; }

        public int CustomerId;
        public Customer Customer { get; set; }

        public DateTime OwnershipStartDate { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
    }
}
