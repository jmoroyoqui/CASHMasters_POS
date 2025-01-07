// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.ErrorHandled
{
    /// <summary>
    /// Represent errors while a transaction
    /// </summary>
    public class TransactionException : Exception
    {
        public TransactionException(string message) : base(message)
        {
            
        }
    }
}
