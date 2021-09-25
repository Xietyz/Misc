using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GsaTest.Core.Model
{
    public class Strategy
    {
        public Strategy()
        {
            Capital = new HashSet<Capital>();
            Pnl = new HashSet<Pnl>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StrategyId { get; set; }
        public string StratName { get; set; }
        public string Region { get; set; }

        public ICollection<Capital> Capital { get; set; }
        public ICollection<Pnl> Pnl { get; set; }
    }
}
