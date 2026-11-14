using EFCore_Session04.Contexts;
using EFCore_Session04.Enums;
using EFCore_Session04.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Session04.Services
{
    internal class BankService
    {
        private BankDbContext _context;

        public BankService(BankDbContext context)
        {
            _context = context;
        }

        private static string ReadRequiredString(string message)
        {
            while (true)
            {
                Console.Write(message);

                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("This field is required. Try again.");
            }
        }

        public async Task AddCustomer()
        {
            Console.WriteLine("--- Add New Customer ---");

            string fullName = ReadRequiredString("Full Name : ");
            string nationalId = ReadRequiredString("National ID : ");

            Console.Write("Date of Birth : (yyyy-MM-dd) ");
            DateOnly dob;
            while (!DateOnly.TryParseExact(
                        Console.ReadLine(), "yyyy-MM-dd", out dob)
                         || dob > DateOnly.FromDateTime(DateTime.Today))
            {
                Console.Write("Invalid date. Enter a valid birth date: ");
            }

            string email = ReadRequiredString("Email         : ");

            string phone = ReadRequiredString("Phone         : ");

            string address = ReadRequiredString("Address       : ");

            Console.WriteLine("Customer Type:");
            Console.WriteLine("    1) Individual");
            Console.WriteLine("    2) Business");
            Console.Write("  Choice: ");

            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice)
                   || (choice != 1 && choice != 2))
            {
                Console.Write("Invalid choice. Enter 1 or 2: ");
            }

            var customer = new Customer
            {
                FullName = fullName,
                NationalID = nationalId,
                DateOfBirth = dob,
                Email = email,
                PhoneNumber = phone,
                Address = address,

                CustomerType = choice == 1
                    ? CustomerType.Individual
                    : CustomerType.Business
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Customer Created successfully. CustomerId = {customer.Id}");

        }

        public async Task OpenAccount()
        {
            Console.WriteLine("--- Open New Account ---");


            Console.WriteLine("Account Number: ");
            int accountNumber;
            while (!int.TryParse(Console.ReadLine(), out accountNumber))
                Console.Write("Account number must be Integer ");

            // Account Type
            Console.WriteLine("Account Type:");
            Console.WriteLine("    1) Savings");
            Console.WriteLine("    2) Current");
            Console.WriteLine("    3) Business");
            Console.Write("  Choice: ");

            int accountTypeChoice;

            while (!int.TryParse(Console.ReadLine(), out accountTypeChoice)
                   || accountTypeChoice < 1
                   || accountTypeChoice > 3)
            {
                Console.Write("Invalid choice. Enter 1, 2 or 3: ");
            }

            Console.WriteLine("Branch Code: ");
            int branchCode;
            while (!int.TryParse(Console.ReadLine(), out branchCode))
                Console.Write("Branch Code must be Integer ");

            // Customer ID
            Console.Write("Customer Id    : ");

            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId)
                   || customerId <= 0)
            {
                Console.Write("Invalid Customer ID. Try again: ");
            }

            // Ownership Type
            Console.WriteLine("Ownership Role:");
            Console.WriteLine("    1) Primary");
            Console.WriteLine("    2) CoHolder");
            Console.Write("  Choice: ");

            int ownershipChoice;

            while (!int.TryParse(Console.ReadLine(), out ownershipChoice)
                   || (ownershipChoice != 1 && ownershipChoice != 2))
            {
                Console.Write("Invalid choice. Enter 1 or 2: ");
            }

            // Validate Branch and Customer
            Console.WriteLine($"Validating branch '{branchCode}' and customer #{customerId}...");
            bool branchExists = await _context.Branches.AnyAsync(b => b.Code == branchCode);

            if (!branchExists)
            {
                Console.WriteLine("Branch not found!");
                return;
            }

            bool customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);

            if (!customerExists)
            {
                Console.WriteLine("Customer not found!");
                return;
            }

            // Check if the account number already exists
            bool accountExists = await _context.Accounts
                .AnyAsync(a => a.AccountNumber == accountNumber);

            if (accountExists)
            {
                Console.WriteLine("Account number already exists!");
                return;
            }

            // Create Account
            var account = new Account
            {
                AccountNumber = accountNumber,
                AccountType = (AccountType)accountTypeChoice,
                BranchId = branchCode,
                CurrentBalance = 0,
                OpeningDate =DateTime.Today
            };

            // Create ownership relationship
            var accountCustomer = new AccountCustomer
            {
                AccountId = accountNumber,
                CustomerId = customerId,
                OwnershipType = (OwnershipType)ownershipChoice,
                OwnershipStartDate = DateTime.Today,
                AccountStatus = AccountStatus.Active
            };

            _context.Accounts.Add(account);
            _context.CustomerAccounts.Add(accountCustomer);

            await _context.SaveChangesAsync();

            Console.WriteLine(
                $"Account '{accountNumber}' created and linked " +
                $"to customer {customerId} as " +
                $"{accountCustomer.OwnershipType} owner.");


        }

        public async Task UpdateAccountStatus()
        {
            Console.WriteLine("--- Update Account Status ---");
            Console.WriteLine("Account Number: ");
            int accountNumber;
            while (!int.TryParse(Console.ReadLine(), out accountNumber)) 
            { 
                Console.Write("Account number must be Integer ");
            }

            Console.WriteLine("Customer ID: ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
            {
                Console.Write("Invalid Customer ID. Try again: ");
            }

            var customerAccount = await _context.CustomerAccounts.FirstOrDefaultAsync(ca => ca.CustomerId == customerId && ca.AccountId == accountNumber);
            if (customerAccount == null)
            {
                Console.WriteLine("This customer does not own this account!");
                return;
            }

            Console.WriteLine("New Status: ");
            Console.WriteLine("    1) Active");
            Console.WriteLine("    2) Closed");
            int choice;
            while(!int.TryParse(Console.ReadLine(),out choice) || (choice!=1 && choice!=2))
                Console.WriteLine("Invalid Choice");

            customerAccount.AccountStatus = choice == 1 ? AccountStatus.Active : AccountStatus.Closed;
            await _context.SaveChangesAsync();
            Console.WriteLine($"Status Updated Successfully to {customerAccount.AccountStatus}");
        }

        public async Task RemoveAccount()
        {
            Console.WriteLine("--- Remove Account From Customer ---");

            Console.Write("Account Number: ");

            int accountNumber;

            while (!int.TryParse(Console.ReadLine(), out accountNumber)
                   || accountNumber <= 0)
            {
                Console.Write("Invalid Account Number. Try again: ");
            }

            Console.Write("Customer ID: ");

            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId)
                   || customerId <= 0)
            {
                Console.Write("Invalid Customer ID. Try again: ");
            }

            var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(ca =>
                    ca.CustomerId == customerId &&
                    ca.AccountId == accountNumber);

            if (customerAccount == null)
            {
                Console.WriteLine("This customer does not own this account!");
                return;
            }

            // Check if the account has other owners
            bool hasOtherOwners = await _context.CustomerAccounts
                .AnyAsync(ca =>
                    ca.AccountId == accountNumber &&
                    ca.CustomerId != customerId);

            // Remove the ownership link
            _context.CustomerAccounts.Remove(customerAccount);

            // If this was the last owner, remove the account
            if (!hasOtherOwners)
            {
                var account = await _context.Accounts
                    .FindAsync(accountNumber);

                if (account != null)
                {
                    _context.Accounts.Remove(account);
                }
            }

            await _context.SaveChangesAsync();

            Console.WriteLine("Ownership link deleted.");

            if (!hasOtherOwners)
            {
                Console.WriteLine(
                    $"That was the last owner - account " +
                    $"'{accountNumber}' was also removed.");
            }
        }

       public async Task ListCustomersWithAccounts()
{
    Console.WriteLine("--- All Customers ---");

    var customers = await _context.Customers
        .AsNoTracking()
        .Include(c => c.AccountCustomers)
        .ThenInclude(ca => ca.Account)
        .ToListAsync();

    if (!customers.Any())
    {
        Console.WriteLine("No customers found.");
        return;
    }

    foreach (var customer in customers)
    {
        Console.WriteLine(
            $"#{customer.Id} {customer.FullName} ({customer.CustomerType})");

        if (!customer.AccountCustomers.Any())
        {
            Console.WriteLine("    (no accounts)");
        }
        else
        {
            foreach (var accountCustomer in customer.AccountCustomers)
            {
                var account = accountCustomer.Account;

                Console.WriteLine(
                    $"    {account.AccountNumber}" +
                    $" | {account.AccountType}" +
                    $" | {accountCustomer.OwnershipType}" +
                    $" | {accountCustomer.AccountStatus}" +
                    $" | Balance: {account.CurrentBalance:N2}");
            }
        }

        Console.WriteLine();
    }
}

    }
}
