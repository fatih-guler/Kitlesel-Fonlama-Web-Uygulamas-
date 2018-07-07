using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.ViewModels
{
    public class FundOfProjectViewModel
    {

        [Required]
        public string ccCardNumber { get; set; }
       [Required]
        public int ccCVC { get; set; }
        [Required]
        public string  ccExpiryDate  { get; set; }
        [Required]
        public decimal ccAmount { get; set; }
        [Required]
        public string ccFullName { get; set; }
         

    }
}
