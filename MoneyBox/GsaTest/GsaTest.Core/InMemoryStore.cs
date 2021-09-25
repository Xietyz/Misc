using System;
using System.Collections.Generic;
using System.Text;
using GsaTest.Core.Model;

namespace GsaTest.Core
{
    public class InMemoryStore
    {
        public List<Strategy> Strategy { get; set; }
        public List<Pnl> Pnl { get; set; }
    }
}