using CASHMasters_POS.ErrorHandled;
using CASHMasters_POS.Management;
using CASHMasters_POS.Misc;
using CASHMasters_POS.Payments;

Configuration configuration = new Configuration();
ConsoleInputValidation validation = new ConsoleInputValidation();
Transaction transaction = new Transaction(configuration.TheCurrency);


do
{
    Console.Clear();
    Console.WriteLine("Cash Master - Point-Of-Sale");
    Console.WriteLine($"Currency: {configuration.TheCurrency.CurrencyCode}");

    Console.WriteLine("Select an option");
    Console.WriteLine("1). Make a transaction.");
    Console.WriteLine("2). Exit.");
    Console.Write("Choose an option: ");

    try
    {
        int option = validation.ConvertToInt(Console.ReadLine());

        if (option > 2 || option < 0)
        {
            Console.WriteLine("Invalid option, try again.");
            continue;
        }
        if (option == 2) break;

        var amounts = transaction.BeginTransaction();
        var validTransaction = transaction.ValidateTransaction(amounts);
        transaction.GetChangeDue(amounts);
    }
    catch (ArgumentException aex)
    {
        Console.WriteLine($"Error. {aex.Message}");
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        continue;
    }
    catch(TransactionException tex)
    {
        Console.WriteLine($"Error. {tex.Message}");
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        continue;
    }
    catch (Exception ex) 
    {
        Console.WriteLine($"Error. {ex.Message}");
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        continue;
    }
} while (true);


Console.WriteLine("Press any key to exit");
Console.ReadKey();