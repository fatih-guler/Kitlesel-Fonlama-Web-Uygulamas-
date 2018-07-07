using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.ViewModels
{
    public class RegisterViewModel
    {
        [MaxLength(20, ErrorMessage = "Username alanı max. 20 karakter olmalıdır."),
        MinLength(6,ErrorMessage="Username alanı Min. 6 karakter olmalıdır."),
        Required(ErrorMessage = "*Bu Alan Boş Geçilemez!.")]
        public string UserName { get; set; }


        [MaxLength(20, ErrorMessage = "Username alanı max. 20 karakter olmalıdır."),
        MinLength(6,ErrorMessage="Parola Alanı Min. 6 karakterden oluşmalıdır."),
        Required(ErrorMessage = "*Bu Alan Boş Geçilemez!"),
        DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(20, ErrorMessage = "Username alanı max. 20 karakter olmalıdır."),
        MinLength(6, ErrorMessage = "Parola Tekrar Alanı Min. 6 karakterden oluşmalıdır."),
        Required(ErrorMessage = "*Bu Alan Boş Geçilemez!"), 
        Compare("Password", ErrorMessage = "Parola ile Parola Tekrar Alanları Uyuşmuyor."),
        DataType(DataType.Password)]
        public string Re_Password { get; set; }

        [Required(ErrorMessage = "*Bu Alan Boş Geçilemez!"),
        DataType(DataType.EmailAddress, ErrorMessage = "E-mail Adresi Geçersizdir.")]
        public string Email { get; set; }

        public bool IsInvestor = false;

    }
}
