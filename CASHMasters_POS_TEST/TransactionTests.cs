// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using CASHMasters_POS.ErrorHandled;
using CASHMasters_POS.Management;
using CASHMasters_POS.Misc;
using CASHMasters_POS.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS_TEST
{
    public class TransactionTests
    {
        Currency currency = new Currency() 
        { 
            CurrencyCode = "USD", 
            Amounts = new List<decimal>() { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m } 
        };      
        
        /// <summary>
        /// It tests the validation for a correct tuple values.
        /// </summary>
        [Fact]
        public void ValidateTransaction_MustReturnTrue()
        {
            var transaction = new Transaction(currency);
            // tuple => item1: price, item2: cash
            (decimal, decimal) tuple = (524, 620);

            bool result = transaction.ValidateTransaction(tuple);

            Assert.Equal(true, result);
        }

        /// <summary>
        /// It tests the validation for an incorrect tuple values. for this test cash is less than price and returns a TransactionException
        /// </summary>
        [Fact]
        public void ValidateTransaction_MustReturnExceptionLowerCashThanPrice()
        {
            var transaction = new Transaction(currency);
            // tuple => item1: price, item2: cash
            (decimal, decimal) tuple = (620, 524);

            var result = Assert.Throws<TransactionException>(() => transaction.ValidateTransaction(tuple));

            Assert.Equal("Invalid transaction, cash is lower than price.", result.Message);
        }

        /// <summary>
        /// It tests the validation for an incorrect tuple values. for this test price and are negative, it returns a TransactionException
        /// </summary>
        [Fact]
        public void ValidateTransaction_MustReturnExceptionNegativeAmount()
        {
            var transaction = new Transaction(currency);

            // tuple => item1: price, item2: cash
            (decimal, decimal) tuple = (-50, -40);

            var result = Assert.Throws<TransactionException>(() => transaction.ValidateTransaction(tuple));

            Assert.Equal("Invalid transaction, cash and/or price is a negative amount.", result.Message);
        }

        /// <summary>
        /// It tests the validation for an incorrect tuple values. for this test price is zero, returns a TransactionException
        /// </summary>
        [Fact]
        public void ValidateTransaction_MustReturnExceptionPriceZero()
        {
            var transaction = new Transaction(currency);

            // tuple => item1: price, item2: cash
            (decimal, decimal) tuple = (0, 152);

            var result = Assert.Throws<TransactionException>(() => transaction.ValidateTransaction(tuple));

            Assert.Equal("Invalid transaction, price cannot be zero.", result.Message);
        }

        /// <summary>
        /// It tests the method with a correct change due
        /// </summary>
        [Fact]
        public void SmallestBillsAndCoins_MustReturnDenominationCollection()
        {
            var transaction = new Transaction(currency);

            decimal changeDue = 1234.55m;

            var result = transaction.SmallestBillsAndCoins(changeDue);

            var expected = new Dictionary<decimal, int>()
            {
                { 100.00m, 12 },
                { 20.00m, 1 },
                { 10.00m, 1 },
                { 2.00m, 2 },
                { 0.50m, 1 },
                { 0.05m, 1 }
            };

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// It tests the method with a bad change due format, it returns a TransactionException.
        /// </summary>
        [Fact]
        public void SmallestBillsAndCoins_MustReturnExceptionDenominationLessThanMinimum()
        {
            var transaction = new Transaction(currency);

            decimal changeDue = 1234.555m;

            var result = Assert.Throws<TransactionException>(() => transaction.SmallestBillsAndCoins(changeDue));

            string expectedMessage = $"Invalid transaction, currency {currency.CurrencyCode} does not contains a denomination minor than {currency.Amounts[0]} to completed it.";
            Assert.Equal(expectedMessage, result.Message);
        }
    }
}
