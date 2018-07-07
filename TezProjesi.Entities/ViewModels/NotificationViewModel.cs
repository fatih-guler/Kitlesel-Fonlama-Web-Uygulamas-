using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.ViewModels
{
    public class NotificationViewModel
    {
        public int Type { get; set; }
        public int FromWho { get; set; }
        public int ToWho { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        /*////////////////*/
        public int projectid { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
}
