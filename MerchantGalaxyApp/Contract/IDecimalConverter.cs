using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Contract
{
    public interface IDecimalConverter
    {
        double? ToDecimal(string input);
    }
}
