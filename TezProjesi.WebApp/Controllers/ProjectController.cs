using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TezProjesi.BusinessLayer;
using TezProjesi.BusinessLayer.Results;
using TezProjesi.Entities;
using TezProjesi.Entities.ViewModels;

namespace TezProjesi.WebApp.Controllers
{
    public class ProjectController : Controller
    {
        NotificationManager nm = new NotificationManager();
        ProjectManager pm = new ProjectManager();

        // GET: Project
        public ActionResult Show(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TempData["projectid"] = id;
            Project project = pm.Find(x => x.Id == id);
            TempData["projectOwner"] = project.user.UserName;
            TempData["projectImage"] = "/ProjectImages/" + id.ToString() + ".jpg";
            TempData["addresses"] = project.user.addresses; 
            if (project == null)
            {
                return new HttpNotFoundResult();
            }
            return View(project);

        }
        public ActionResult AddProject()
        {
            if (Session["user"] == null || (Session["user"] as User).IsInvestor)
            {
                ViewBag.Error = null;
                TempData["Error"] = "AddProject";
                return RedirectToAction("ErrorPage", "Home");

            }
            else
                return View(new Project());

        }

        public ActionResult ProjectDetails(int project_id)
        {
            Project p = pm.List().Where(x => x.Id == project_id).FirstOrDefault();
            if (p != null)
                return View(p);
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult AddProjectJson(Project project, List<Reward> rewards)
        {

            int response = -1;
            project.user = (Session["user"] as User);
            /*      for (int i = 0; i < project.Rewards.Count; i++)
                  {
                      project.Rewards[i].Project = project;
                      project.Rewards[i].ProjectId = project.Id;
                  }
             * */
            if (ModelState.IsValid)
            {

                project.ImageOfProject = new byte[4];
                project.Rewards = rewards;
                Response.Write("<script>" + Request.Files.Count + "</script>");
                ProjectManager pm = new ProjectManager();
                response = pm.AddProject(project);
                //HttpPostedFileBase file = Request.Files["ImageOfProject"];
                //string path = System.IO.Path.Combine(Server.MapPath("~/ProjectImages/"+response));
                //file.SaveAs(path);
                Session["addingImageId"] = response;
                return Json(new { res = response }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { res = response }, JsonRequestBehavior.DenyGet);
            //   return Json(response);
        }
        [HttpPost]
        public void UploadImage()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/ProjectImages/"), Session["addingImageId"].ToString() + ".jpg");
                Session["addingImageId"] = null;
                file.SaveAs(path);
            }
        }

        public ActionResult MyProject()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            else
            {
                if ((Session["user"] as User).IsInvestor == false)
                {
                    UserManager um = new UserManager();
                    int Ses_id = (Session["user"] as User).Id;
                    ProjectManager pm = new ProjectManager();
                    var Projects = pm.List(x => x.user.Id == Ses_id);
                    List<FundViewModel> fvm = new List<FundViewModel>();
                    foreach (var item in Projects)
                    {
                        fvm.Add(new FundViewModel()
                        {
                            Id = item.Id,
                            ProjectAmount = item.ProjectAmount,
                            ProjectDate = item.ProjectDuration,
                            ProjectName = item.ProjectName,
                            ProjectTitle = item.ProjectTitle
                        });
                    }
                    return View((fvm));
                }
                else
                {
                    List<FundViewModel> fundModel = new List<FundViewModel>();
                    ProjectManager pm = new ProjectManager();
                    UserManager um = new UserManager();
                    int sessionId = (Session["user"] as User).Id;
                    FundManager fm = new FundManager();
                    List<Fund> funds = fm.List(x => x.user_Investor.Id == sessionId);
                    List<Project> projects = new List<Project>();
                    foreach (Fund element in funds)
                    {
                        FundViewModel fvm = new FundViewModel();
                        Project p = pm.Find(x => x.Id == element.Project.Id);

                        fvm.ProjectAmount = element.Amount;
                        fvm.ProjectDate = element.Date;
                        fvm.ProjectName = p.ProjectName;
                        fvm.ProjectTitle = p.ProjectTitle;
                        fvm.Id = p.Id;
                        fundModel.Add(fvm);
                    }
                    return View(fundModel);
                }
            }
        }
        [HttpPost]
        public ActionResult MyProject(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyAddComment(string comment_title, string comment_content, int project_id)
        {
            ProjectManager pm = new ProjectManager();
            Notification not = new Notification();
            bool result = false;
            int userid = (Session["user"] as User).Id;
            CommentManager cm = new CommentManager();
            result = cm.AddComment(project_id, userid, comment_content, comment_title);
            if (!result)
            {
                not.Type = 0;
                not.FromWho = userid;
                not.Comment = comment_content;
                not.Date = DateTime.Now;
                not.ToWho = -1;
                not.projectid = project_id;
                nm.Insert(not);
                ViewBag.CommentResult = "Yorum ekleme işlemi başarılı";
                ViewBag.Color = "success";
            }
            else
            {
                ViewBag.CommentResult = "Yorum Eklerken Bir Hata Meydana Geldi";
                ViewBag.Color = "danger";
            }
            TempData["projectid"] = project_id;
            Project project = pm.Find(x => x.Id == project_id);
            TempData["projectOwner"] = project.user.UserName;
            TempData["projectImage"] = "/ProjectImages/" + project_id.ToString() + ".png";
            return RedirectToAction("Show", "Project", pm.Find(x => x.Id == project_id));
        }
        [HttpPost]
        public JsonResult RemoveComment(int commentId)
        {
            bool result = false;
            CommentManager cm = new CommentManager();
            result = cm.RemoveComment(commentId);
            return Json(new { isRemoved = result });
        }
        [HttpPost]
        public JsonResult ConfirmComment(int commentId)
        {
            bool result = false;
            CommentManager cm = new CommentManager();
            result = cm.ConfirmComment(commentId);
            return Json(new { isConfirmed = true });
        }

        [HttpPost]
        public ActionResult ProjectFunds(int project_id)
        {

            {
                FundManager fd = new FundManager();
                List<Fund> list_fd = fd.List(x => x.Project.Id == project_id);
                return View(list_fd);
            }
        }
        public ActionResult ProjectComments(int project_id)
        {

            CommentManager cm = new CommentManager();
            List<Comment> Comments = cm.List(x => x.Project.Id == project_id && x.IsConfirmed == false);
            return View(Comments);
        }
        public ActionResult RequestFollow(int following_id)
        {
            FollowManager fm = new FollowManager();
            Follow f = new Follow()
            {
                Following = following_id,
                Follower = (Session["user"] as User).Id,
            };
            fm.Insert(f);
            Notification not = new Notification()
            {
                Comment = "",
                Date = DateTime.Now,
                FromWho = (Session["user"] as User).Id,
                ToWho = following_id,
                Type = 2,
                projectid = -1,
            };

            nm.Insert(not);
            return View();
        }
        [HttpGet]
        public ActionResult GetNotification()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<Notification> Notifications = new List<Notification>();
                int sesis = (Session["user"] as User).Id;
                List<Project> projects = pm.List(x => x.user.Id == sesis).ToList();
                foreach (Project item in projects)
                {
                    List<Notification> n = nm.List(x => x.projectid == item.Id && x.FromWho != sesis).Where(x => x.projectid == item.Id).ToList();
                    Notifications.AddRange(n);
                }
                List<Notification> followNotifications = nm.List(x => x.ToWho == sesis);
                Notifications.AddRange(followNotifications);
                List<NotificationViewModel> notificationModel = new List<NotificationViewModel>();
                UserManager um = new UserManager();
                ProjectManager pManager = new ProjectManager();
                foreach (var item in Notifications)
                {
                    var userId = item.FromWho;
                    var userName = um.Find(x => x.Id == userId).UserName;
                    var projectName = pManager.Find(x => x.Id == item.projectid).ProjectName;
                    NotificationViewModel n = new NotificationViewModel()
                    {
                        Type = item.Type,
                        Comment = item.Comment,
                        Date = item.Date,
                        FromWho = item.FromWho,
                        projectid = item.projectid,
                        UserName = um.Find(x => x.Id == item.FromWho).UserName,
                        ProjectName = projectName,
                        ToWho = item.ToWho
                    };
                    notificationModel.Add(n);
                }
                return View(notificationModel.OrderByDescending(x => x.Date));
            }
        }
        public ActionResult GetPartialNotification()
        {
            List<Notification> Notifications = new List<Notification>();
            int sesis = (Session["user"] as User).Id;
            List<Project> projects = pm.List(x => x.user.Id == sesis).ToList();
            foreach (Project item in projects)
            {
                List<Notification> n = nm.List(x => x.projectid == item.Id && x.FromWho != sesis).Where(x => x.projectid == item.Id).ToList();
                Notifications.AddRange(n);
            }
            List<Notification> followNotifications = nm.List(x => x.ToWho == sesis);
            Notifications.AddRange(followNotifications);
            List<NotificationViewModel> notificationModel = new List<NotificationViewModel>();
            UserManager um = new UserManager();
            ProjectManager pManager = new ProjectManager();
            foreach (var item in Notifications)
            {
                var userId = item.FromWho;
                var userName = um.Find(x => x.Id == userId).UserName;
                var projectName = pManager.Find(x => x.Id == item.projectid).ProjectName;
                NotificationViewModel n = new NotificationViewModel()
                {
                    Type = item.Type,
                    Comment = item.Comment,
                    Date = item.Date,
                    FromWho = item.FromWho,
                    projectid = item.projectid,
                    UserName = userName,
                    ProjectName = projectName,
                    ToWho = item.ToWho
                };
                notificationModel.Add(n);
            }
            return PartialView("_PartialNotification", notificationModel);
        }
        public ActionResult FundOfProject(int project_id)
        {
            if (Session["user"] == null || !(Session["user"] as User).IsInvestor)
            {

                ViewBag.Error = null;
                TempData["Error"] = "FundProject";
                return RedirectToAction("ErrorPage", "Home");

            }
            else
            {
                TempData["fundingProjectId"] = project_id;
                return View(new FundOfProjectViewModel());
            }
        }
        [HttpPost]
        public ActionResult FundOfProject(FundOfProjectViewModel model, int project_id)
        {
            FundManager fm = new FundManager();
            UserManager um = new UserManager();
            User user = new User();
            int sesid = (Session["user"] as User).Id;
            user = um.Find(x => x.Id == sesid);
            Project p = pm.Find(x => x.Id == project_id);
            bool result = fm.AddFund(model, user, p);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        [ValidateInput(false)]
        [ChildActionOnly]
        public ActionResult GetQuestionsOfProject(int projectid)
        {
            ProjectManager pm = new ProjectManager();
            Project which = pm.Find(x => x.Id == projectid);
            ViewBag.ProjectId = projectid;
            return PartialView("_PartialGetQuestions", which.Questions);
        }
        [HttpPost]
        public JsonResult AffixQuestion(int projectid, string questioncontent)
        {
            if (Session["user"] != null)
            {
                User sesid = Session["user"] as User;
                QuestionManager qm = new QuestionManager();
                NotificationManager nm = new NotificationManager();
                nm.Insert(new Notification { Type = 1, Comment = questioncontent, Date = DateTime.Now, FromWho = (Session["user"] as User).Id, ToWho = -1, projectid = projectid });
                ProjectManager pm = new ProjectManager();
                Project project = pm.Find(x => x.Id == projectid);
                Question q = new Question()
                {
                    Project = project,
                    user = sesid,
                    QuestionDescription = questioncontent
                };
                int sonuc = qm.Insert(q);
                if (sonuc > 0)
                {
                    return Json(new { isAdded = true, msg = "Soru Başarıyla Eklendi" });
                }

                return Json(new { isAdded = false, msg = "Soru Eklenirken bir hata meydana geldi" });
            }
            else
                return Json(new { isAdded = false, msg = "Soru ekleyebilmek için giriş yapmalısınız" });

        }
        [HttpPost]
        public JsonResult AffixAnAnswer(string AnswerDescription, int questionid, int projectid, int askerId)
        {

            QuestionManager qm = new QuestionManager();
            ProjectManager pm = new ProjectManager();
            Project project = pm.Find(x => x.Id == projectid);

            Question q = qm.Find(x => x.Id == questionid);
            AnswerManager anm = new AnswerManager();
            NotificationManager nm = new NotificationManager();
            Notification not = new Notification()
            {
                Type = 4,
                ToWho = askerId,
                projectid = projectid,
                Date = DateTime.Now,
                Comment = AnswerDescription,
                FromWho = (Session["user"] as User).Id
            };

            Answer answer = new Answer()
            {
                AnswerDescription = AnswerDescription,
                question = q,
                user = Session["user"] as User,

            };
            nm.Insert(not);

            int result = anm.Insert(answer);
            if (result > 0)
            {
                return Json(new { isAdded = true, msg = "İşlem Başarılı" });
            }
            return Json(new { isAdded = false, msg = "Başarısız" });
        }
        public ActionResult MyAddressInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyAddressInfo(Address address)
        {
            AddressManager ad = new AddressManager();
            User sesid = Session["user"] as User;
            address.user = sesid;
            BusinessLayerResult<Address> res_adr = new BusinessLayerResult<Address>();
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(address.City) || String.IsNullOrEmpty(address.Country) || String.IsNullOrEmpty(address.Description) || String.IsNullOrEmpty(address.District))
                {
                    ViewBag.Status = "danger";
                    ViewBag.Result = "Adres Ekleme İşlemi Başarısız. Eksik alan bırakmayınız";
                }
                else
                {
                    int result = ad.Insert(address);
                    if (result > 0)
                    {
                        ViewBag.Result = "Adres Ekleme İşleminiz Başarılı.";
                        ViewBag.Status = "success";
                    }
                    else
                    {
                        ViewBag.Status = "danger";
                        ViewBag.Result= "Adres Ekleme İşlemi Başarısız.";
                    }
                }

            }
            else
            {
                return View("ErrorPage");
            }

            return View();
        }
        public PartialViewResult GetMyAddress()
        {
            AddressManager adm = new AddressManager();
            int sesid = (Session["user"] as User).Id;
            IEnumerable<Address> address = adm.List(x => x.user.Id == sesid);
            return PartialView("_PartialMyAddresses", address);
        }
    }
}