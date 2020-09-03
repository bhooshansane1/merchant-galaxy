using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Contract
{
    public interface IExpression
    {
        bool Match(string input);
        void Execute(string input);
    }
}
