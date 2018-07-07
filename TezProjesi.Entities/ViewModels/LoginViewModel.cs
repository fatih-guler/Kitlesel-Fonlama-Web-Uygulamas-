using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*Bu Alan Boş Geçilemez!.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*Bu Alan Boş Geçilemez!."),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
