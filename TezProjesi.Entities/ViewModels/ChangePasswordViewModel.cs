using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.ViewModels
{
    public class ChangePasswordViewModel
    {
         [MaxLength(20, ErrorMessage = "Eski Parola Alanı max. 20 karakter olmalıdır."),
        MinLength(6, ErrorMessage = "Eski Parola Min. 6 karakterden oluşmalıdır."),
        Required(ErrorMessage = " Eski Parola Alanı Boş Geçilemez!"),
        DataType(DataType.Password)]
        public string OldPassword { get; set; }

         [MaxLength(20, ErrorMessage = "Yeni Parola Alanı max. 20 karakter olmalıdır."),
        MinLength(6, ErrorMessage = "Yeni Parola Min. 6 karakterden oluşmalıdır."),
        Required(ErrorMessage = " Yeni Parola Alanı Boş Geçilemez!"),
        DataType(DataType.Password)]
        public string NewPassword { get; set; }

         [MaxLength(20, ErrorMessage = "Yeni Parola Tekrar Alanı max. 20 karakter olmalıdır."),
        MinLength(6, ErrorMessage = "Yeni Parola Tekrar Min. 6 karakterden oluşmalıdır."),
        Required(ErrorMessage = " Yeni Parola Tekrar Alanı Boş Geçilemez!"),
        Compare("NewPassword", ErrorMessage = "Parola ile Parola Tekrar Alanları Uyuşmuyor."),
        DataType(DataType.Password)]
        public string Re_NewPassword { get; set; }
    }
}
