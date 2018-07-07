using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.ViewModels
{
    public class FundViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectTitle { get; set; }
        public decimal ProjectAmount { get; set; }
        public DateTime ProjectDate { get; set; }
    }
}
