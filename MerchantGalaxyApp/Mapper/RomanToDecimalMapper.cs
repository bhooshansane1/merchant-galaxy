using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxyApp.Mapper
{
    public class RomanToDecimalMapper
    {
        public enum RomToDecMapper
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
        }

        readonly Dictionary<string, RomToDecMapper> mapping;

        public RomanToDecimalMapper()
        {
            mapping = new Dictionary<string, RomToDecMapper>
            {
                { RomToDecMapper.I.ToString(), RomToDecMapper.I },
                { RomToDecMapper.V.ToString(), RomToDecMapper.V },
                { RomToDecMapper.X.ToString(), RomToDecMapper.X },
                { RomToDecMapper.L.ToString(), RomToDecMapper.L },
                { RomToDecMapper.C.ToString(), RomToDecMapper.C },
                { RomToDecMapper.D.ToString(), RomToDecMapper.D },
                { RomToDecMapper.M.ToString(), RomToDecMapper.M }
            };
        }

        public RomToDecMapper GetValue(string val)
        {
            return mapping[val.ToUpper()];
        }
    }
}
