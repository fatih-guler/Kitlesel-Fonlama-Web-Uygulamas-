using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities
{
    public class Notification:MyEntityBase
    {
        // 0 -> yorum, 1 -> soru sorma, 2-> takip, 3-> yatırım
        public int Type { get; set; }
        public int FromWho { get; set; }
        public int ToWho { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        /*////////////////*/
        public int projectid { get; set; }
       
        
    }
}
