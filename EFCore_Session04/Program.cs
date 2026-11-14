using EFCore_Session04.Contexts;
using EFCore_Session04.Services;

namespace EFCore_Session04
{
  
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new BankDbContext();
            var bankService = new BankService(context);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("===============================");
                Console.WriteLine("   National Bank - Management");
                Console.WriteLine("===============================");

                Console.WriteLine("1) Add a new Customer");
                Console.WriteLine("2) Open a new Account");
                Console.WriteLine("3) Update Account Status");
                Console.WriteLine("4) Remove an Account from a Customer");
                Console.WriteLine("5) List all Customers with Accounts");
                Console.WriteLine("0) Exit");

                Console.WriteLine("===============================");
                Console.Write("Enter choice: ");

                bool isParsed = int.TryParse(
                    Console.ReadLine(), out int choice);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await bankService.AddCustomer();
                        break;

                    case 2:
                        await bankService.OpenAccount();
                        break;

                    case 3:
                        await bankService.UpdateAccountStatus();
                        break;

                    case 4:
                        await bankService.RemoveAccount();
                        break;

                    case 5:
                        await bankService.ListCustomersWithAccounts();
                        break;

                    case 0:
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");

                Console.ReadKey();
            }
        }
    }
}
