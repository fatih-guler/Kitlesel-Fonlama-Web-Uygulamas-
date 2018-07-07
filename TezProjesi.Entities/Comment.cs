using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    public class Comment:MyEntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime Date { get; set; }
        //Mapping
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }

    }
}
