using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.WebApp.Models
{
    public class LoginRegisterWebAppModel
    {
        
        public RegisterViewModel registerviewmodel { get; set; }
        public LoginViewModel loginviewmodel { get; set; }
    }
}