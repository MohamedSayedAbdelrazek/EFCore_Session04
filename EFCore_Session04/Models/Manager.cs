using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Models
{
    internal class Manager
    {
        public int Id { get; set; }
        public string FullName {get;set;} = String.Empty;
        public string Email {get;set;} = String.Empty;
        public string PhoneNumber {get;set;} = String.Empty;
        public DateOnly HireDate {get;set;}

        //Manage Relationship
        public Branch Branch {get;set;}
    }
}
