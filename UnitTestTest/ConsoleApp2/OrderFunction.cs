using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public class OrderFunction
    {
        public string[] OrderByAssumingSpecial(string[] testdata)
        {
            return testdata.OrderBy(x => SpecialLength(x.Length)).ToArray();
        }

        private int SpecialLength(int length)
        {
            if (length % 2 == 0)
            {
                return length * 2;
            }

            return length;
        }
    }
}
