using EFCore_Session04.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Models
{
    [PrimaryKey(nameof(TransactionNumber))]
    internal class Transaction
    {
        public int TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Note { get; set; } = String.Empty;

        //Related Relationship
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
