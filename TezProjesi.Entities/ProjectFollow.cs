using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    public class ProjectFollow:MyEntityBase
    {
        
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        


    }
}
