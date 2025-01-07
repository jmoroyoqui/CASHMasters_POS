using CASHMasters_POS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Misc
{
    public class ConsoleInputValidation : ITypeConversionValidation
    {
        /// <summary>
        /// Convert a string value to decimal
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Decimal value</returns>
        /// <exception cref="ArgumentException"></exception>
        public decimal ConvertToDecimal(string value)
        {
            value = string.IsNullOrEmpty(value) ? "0" : value;
            decimal retval;
            if (decimal.TryParse(value, out retval))
            {
                return retval;
            }
            else
            {
                throw new ArgumentException("Cannot parse value to decimal.");
            }
        }

        /// <summary>
        /// Convert a string value to int
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Int value</returns>
        /// <exception cref="ArgumentException"></exception>
        public int ConvertToInt(string value)
        {
            value = string.IsNullOrEmpty(value) ? "0" : value;
            int retval;
            if (int.TryParse(value, out retval))
            {
                return retval;
            }
            else
            {
                throw new ArgumentException("Cannot parse value to int.");
            }
        }
    }
}
