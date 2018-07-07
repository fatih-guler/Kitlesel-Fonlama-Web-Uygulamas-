    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
   public class Fund
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date{ get; set; }
        public virtual User user_Investor { get; set; }
        public virtual  Project Project { get; set; }

    }
}
