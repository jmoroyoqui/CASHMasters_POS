using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHMasters_POS.Interfaces
{
    /// <summary>
    /// Define methods to validate and handle data type convertions
    /// </summary>
    public interface ITypeConversionValidation
    {
        int ConvertToInt(string value);
        decimal ConvertToDecimal(string value);
    }
}
