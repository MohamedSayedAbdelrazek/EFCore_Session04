using EFCore_Session04.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Address { get; set; } = String.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string FullName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public CustomerType CustomerType { get; set; }
        public string NationalID { get; set; } = String.Empty;
        //Have Relationship
        public ICollection<AccountCustomer> AccountCustomers { get; set; } = new HashSet<AccountCustomer>();
    }
}
