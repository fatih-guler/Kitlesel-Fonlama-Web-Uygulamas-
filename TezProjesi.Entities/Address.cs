using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    public class Address:MyEntityBase
    {
       
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        //Mapping
        public virtual User user { get; set; }


    }
}
