// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using CASHMasters_POS.ErrorHandled;
using CASHMasters_POS.Management;
using CASHMasters_POS.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Payments
{
    public class Transaction
    {
        private readonly Currency _currency;

        ConsoleInputValidation validation = new ConsoleInputValidation();
        TransactionValidation transactionValidation = new TransactionValidation();
        public Transaction(Currency currency)
        {
            _currency = currency;
        }

        /// <summary>
        /// This method begins a transaction, it request for the total price of items and then calls to 
        /// GetCashFromCustomer method in order to input the payment as each denomination
        /// </summary>
        /// <returns>Tuple of decimals, item1: totalPrice, item2: cash from customer</returns>
        public (decimal, decimal) BeginTransaction()
        {
            Console.WriteLine("Begin a new transaction.");
            Console.Write("Total price of item(s) purchased: $");
            decimal price = validation.ConvertToDecimal(Console.ReadLine());
            decimal cash = GetCashFromCustomer();

            return (price, cash);
        }

        /// <summary>
        /// This method validates some conditions in order to continue with the transaction.
        /// Validations make sure that the price and cash accomplish with the following sentenes:
        /// Cash is not lower than Price, Price and/or Cash cannot be negative, and the Price cannot be zero.
        /// </summary>
        /// <param name="amounts">Tuple for price and cash (price,cash)</param>
        /// <returns>True if no error found.</returns>
        /// <exception cref="TransactionException"></exception>
        public bool ValidateTransaction((decimal, decimal) amounts)
        {
            decimal price = amounts.Item1;
            decimal cash = amounts.Item2;

            if (transactionValidation.CashLowerThanPrice(price, cash)) throw new TransactionException("Invalid transaction, cash is lower than price.");
            if (transactionValidation.NegativeAmounts(price, cash)) throw new TransactionException("Invalid transaction, cash and/or price is a negative amount.");
            if (transactionValidation.ZeroPrice(price)) throw new TransactionException("Invalid transaction, price cannot be zero.");
            return true;
        }

        /// <summary>
        /// Calculates the optimal change due denominations
        /// </summary>
        /// <param name="amounts">Tuple for price and cash (price,cash)</param>
        public void GetChangeDue((decimal, decimal) amounts)
        {
            decimal price = amounts.Item1;
            decimal cash = amounts.Item2;

            var changeDue = SmallestBillsAndCoins(cash - price);
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Total amount: {price.ToString("c")}");
            Console.WriteLine($"Customer pay: {cash.ToString("c")}");
            Console.WriteLine($"Change due: {(cash - price).ToString("c")}");
            
            foreach (var denomination in changeDue)
            {
                Console.WriteLine($"{denomination.Key.ToString("c")}: {denomination.Value}");
            }
            Console.WriteLine("--------------------------------------");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }


        /// <summary>
        /// Loop the currency denomination collection in order to found the optimal change due.
        /// </summary>
        /// <param name="amount">Change due amount</param>
        /// <returns>Dictionary<decimal,int> that contains the denomination count for bills and coins</returns>
        /// <exception cref="TransactionException"></exception>
        public Dictionary<decimal, int> SmallestBillsAndCoins(decimal amount)
        {
            var changeDue = new Dictionary<decimal, int>();

            for (int i = _currency.Amounts.Count - 1; i >= 0; i--)
            {
                int count = (int)(amount / _currency.Amounts[i]);
                if (count > 0)
                {
                    decimal totalDenomination = (decimal)(count * _currency.Amounts[i]);
                    changeDue.Add(_currency.Amounts[i], count);
                    amount -= totalDenomination;
                }
            }
            if (amount > 0 && _currency.Amounts[0] > amount)
                throw new TransactionException($"Invalid transaction, currency {_currency.CurrencyCode} does not contains a denomination minor than {_currency.Amounts[0]} to completed it.");
            return changeDue;
        }

        /// <summary>
        /// Request each denomination count provided by customer.
        /// </summary>
        /// <returns>Total pay from customer</returns>
        private decimal GetCashFromCustomer()
        {
            Console.WriteLine("\nCash provided by customer. Input all denomination if applies.");
            decimal totalPay = 0.0m;
            for (int i = _currency.Amounts.Count - 1; i >= 0; i--)
            {
                Console.Write($"{_currency.Amounts[i].ToString("c")}: ");
                int count = validation.ConvertToInt(Console.ReadLine());
                totalPay +=(decimal) (count * _currency.Amounts[i]);
            }
            return totalPay;
        }
    }
}
