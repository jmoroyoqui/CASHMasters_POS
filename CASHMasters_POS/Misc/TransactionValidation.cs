// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using CASHMasters_POS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Misc
{
    public class TransactionValidation : ITransactionValidation
    {
        /// <summary>
        /// Determine if cash provided if less than price
        /// </summary>
        /// <param name="price">Total to pay</param>
        /// <param name="cash">Customer pay</param>
        /// <returns>True if the cash is not enough to pay the price, otherwise False</returns>
        public bool CashLowerThanPrice(decimal price, decimal cash) => (cash < price);
        
        /// <summary>
        /// Determine if price and/or cash is a negative amount.
        /// </summary>
        /// <param name="price">Total to pay</param>
        /// <param name="cash">Customer pay</param>
        /// <returns>True if both are negative amounts, otherwise False</returns>
        public bool NegativeAmounts(decimal price, decimal cash) => (price < 0 || cash < 0);
        
        /// <summary>
        /// Determine if price is greather than zero
        /// </summary>
        /// <param name="price">Total to pay</param>
        /// <returns>True if price is equal to zero, otherwise False</returns>
        public bool ZeroPrice(decimal price) => (price == 0);
    }
}
