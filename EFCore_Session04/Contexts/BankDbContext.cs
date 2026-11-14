using EFCore_Session04.Configurations;
using EFCore_Session04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Session04.Contexts
{
    internal class BankDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountCustomer> CustomerAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=BankManagementSystem;Trusted_Connection=True; TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfigurations());

            modelBuilder.ApplyConfiguration(new CustomerConfigurations());

            modelBuilder.ApplyConfiguration(new BranchConfigurations());

            modelBuilder.ApplyConfiguration(new ManagerConfigurations());

            modelBuilder.ApplyConfiguration(new TransactionConfigurations());

            modelBuilder.ApplyConfiguration(new AccountCustomerConfigurations());

        }
    }
}
