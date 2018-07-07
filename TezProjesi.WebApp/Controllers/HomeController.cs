using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using TezProjesi.BusinessLayer;
using TezProjesi.BusinessLayer.Results;
using TezProjesi.Entities;
using TezProjesi.Entities.ViewModels;
using System.Net;

namespace TezProjesi.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ProjectManager pmanager = new ProjectManager();
            List<Project> Projects = pmanager.GetAllProject();
            return View(Projects);
        }

        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string password, string username)
        {
            LoginRegisterManager lgm = new LoginRegisterManager();
            BusinessLayerResult<User> result = new BusinessLayerResult<User>();
            result.Result = lgm.Find(x => x.UserName == username && x.Password == password);
            if (ModelState.IsValid)
            {
                LoginViewModel log_in = new LoginViewModel() { Password = password, UserName = username };
                result = lgm.Login(log_in);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Description));
                }
                else
                {
                    Session["user"] = result.Result;
                }
            }
            if (Session["user"] == null)
            {
                ViewBag.Error = "Login";
                TempData["Error"] = null;
                return View("ErrorPage");// Content("Hatalı kullanıcı adı ya da şifre.!");
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult Register()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");
            else
                return View(new RegisterViewModel());
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel reg_user)
        {
            bool deneme = Request.Form["IsInvestor"] != null && (Request.Form["IsInvestor"].ToString()) == "on";
            reg_user.IsInvestor = deneme;
            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> user = new BusinessLayerResult<User>();
                LoginRegisterManager lgm = new LoginRegisterManager();
                user = lgm.Register(reg_user);
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError("", x.Description));
                    ViewBag.Result = user.Errors[0].ToString();
                    ViewBag.BG = "red";
                }
                else
                {
                    ViewBag.Result = "Kayıt başarıyla eklendi...";
                    ViewBag.BG = "green";
                }

            }
            else
            {
                ViewBag.BG = "red";
                ViewBag.Result = "Kayıt sırasında hata oluştu.";
            }
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string re_newPassword)
        {
            UserManager um = new UserManager();
            int ses_id = (Session["user"] as User).Id;
            bool sonuc = false;
            sonuc = um.ChangePassword(ses_id, oldPassword, newPassword, re_newPassword);
            
            if (sonuc)
            {
                //ViewBag.Sonuc = "Parola Başarıyla Değiştirildi";
                //ViewBag.Color = "success";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = null;
                ViewBag.Error = "ChangePassword";
                return View("ErrorPage");
            }
        }
        public ActionResult EditProfile()
        {
            return View();
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            var isValid = false;
            UserManager um = new UserManager();
            User u = um.Find(x => x.Email == email);
            if (u == null)
                isValid = false;
            else
            {

                var fromAddress = new MailAddress("mailadresiniz@gmail.com");
                var toAddress = new MailAddress(email);
                const string subject = "FundNow Şifre Sıfırlama İsteği";
                using (var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "Mail_Sifresi")
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = "Şifreniz : " + u.Password })
                    {
                        smtp.Send(message);
                    }
                }
                isValid = true;
            }

            if(!isValid)
            {
                ViewBag.Error = "ResetPassword";
                TempData["Error"] = null;
                return View("ErrorPage");
            }
            else
                return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}