// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Management
{
    /// <summary>
    /// Represent each currency json object
    /// </summary>
    public class Currency
    {
        public string CurrencyCode { get; set; }
        public List<decimal> Amounts { get; set; }
    }
}
