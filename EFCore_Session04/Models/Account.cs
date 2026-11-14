using EFCore_Session04.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Models
{
    [PrimaryKey(nameof(AccountNumber))]
    internal class Account
    {
        public int AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime OpeningDate { get; set; }

        //Belong Relationship
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        //Related Relationship
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

        //Have Relationship
        public ICollection<AccountCustomer> AccountCustomers { get; set; } = new HashSet<AccountCustomer>();
    }
}
