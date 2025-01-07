// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Interfaces
{
    /// <summary>
    /// Define methods to validate and handle transactions
    /// </summary>
    public interface ITransactionValidation
    {
        bool NegativeAmounts(decimal price, decimal cash);
        bool ZeroPrice(decimal price);
        bool CashLowerThanPrice(decimal price, decimal cash);
    }
}
