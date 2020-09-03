using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Contract
{
    public interface IRule
    {
        bool Execute(string input);
    }
}
