// ============================================================
// Author: Julio Cesar Moroyoqui Gil
// Date: Jan 07, 2025
// ============================================================

using CASHMasters_POS.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS_TEST
{
    public class ConversionTests
    {
        /// <summary>
        /// Convert string value to decimal, it returns 5.25m as decimal
        /// </summary>
        [Fact]
        public void ConvertToDecimal_MustReturnsDecimalDigits()
        {
            var validation = new ConsoleInputValidation();
            string strValue = "5.25";

            decimal result = validation.ConvertToDecimal(strValue);

            Assert.Equal(5.25m, result);
        }

        /// <summary>
        /// Convert string value to int, it returns 999 as int
        /// </summary>
        [Fact]
        public void ConvertToInt_MustReturns999()
        {
            var validation = new ConsoleInputValidation();
            string strValue = "999";

            int result = validation.ConvertToInt(strValue);

            Assert.Equal(999, result);
        }
    }
}
