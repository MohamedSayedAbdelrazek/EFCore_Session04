using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Models
{
    [PrimaryKey(nameof(Code))]
    internal class Branch
    {
        
        public int Code { get; set; }
        public string Name{get;set;} = String.Empty;
        public string Address{get;set;} = String.Empty;
        public string PhoneNumber{get;set;} = String.Empty;

        //Manage Relationship
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        //Belong Relationship
        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
